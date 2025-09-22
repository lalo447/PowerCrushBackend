using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;

namespace NewSystem.App.Player
{
    public record GetPlayersQuery() : IRequest<Result<List<GetPlayersList>>>;
    internal class GetPlayersHandle(NewSystemContext context) : IRequestHandler<GetPlayersQuery, Result<List<GetPlayersList>>>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="query">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the list of products.</returns>
        public async Task<Result<List<GetPlayersList>>> Handle(GetPlayersQuery query, CancellationToken cancellationToken)
        {
            var data = await context.Players
            .AsNoTracking()
            .OrderByDescending(x => x.Points)
            .Take(10)
            .Select(x =>
                new GetPlayersList(
                x.Id,
                x.Name,
                x.Points))
            .ToListAsync();

            if (data is null)
                return new Error<List<GetPlayersList>>("NotFound", "Players not found");

            return new Ok<List<GetPlayersList>>(data);
        }
    }
    public record GetPlayersList(int Id, string Name, int Points);
}
