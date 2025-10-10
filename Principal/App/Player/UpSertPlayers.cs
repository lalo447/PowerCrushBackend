using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;
using NewSystem.Domain.PowerCrushPlayer;

namespace NewSystem.App.Player
{
    public record UpSertPlayerCommand(string Name, int Points) : IRequest<Result<bool>>;
    internal class UpSertPlayersHandle(NewSystemContext context) : IRequestHandler<UpSertPlayerCommand, Result<bool>>
    {
        /// <summary>Handles the specified command.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns true if you perform the operation.</returns>
        public async Task<Result<bool>> Handle(UpSertPlayerCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name) || command.Points == null)
                return new Error<bool>("NotFound", "Data cannot be null for insertion");

            var player = await context.Players.AsNoTracking().FirstOrDefaultAsync(x => x.Name == command.Name);

            if (player is not null)
            {
                player.Update(command.Points);
                await context.SaveChangesAsync(cancellationToken);

                return new Ok<bool>(true);
            }
                
            Players? playerRegister = Players.Create(command.Name, command.Points);
            context.Add(playerRegister);
            await context.SaveChangesAsync(cancellationToken);

            return new Ok<bool>(true);
        }
    }
}
