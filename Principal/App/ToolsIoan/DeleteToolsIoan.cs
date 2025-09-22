using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.App.ToolsIoan
{
   public record DeleteToolsIoanCommand(int ToolIoanId) : IRequest<Result<DeleteToolsIoan>>;

   internal class DeleteToolsIoanHandler(NewSystemContext context) : IRequestHandler<DeleteToolsIoanCommand, Result<DeleteToolsIoan>>
   {
        /// <summary>Handles the specified command.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns an object if I delete the record correctly.</returns>
        public async Task<Result<DeleteToolsIoan>> Handle(DeleteToolsIoanCommand command, CancellationToken cancellation)
        {
            var data = await context.ToolsIoan.FirstOrDefaultAsync(x => x.ToolIoanId == command.ToolIoanId);

            if (data is null)
                return new Error<DeleteToolsIoan>("NotFound", "The record to be deleted was not found.");

            context.ToolsIoan.Remove(data);
            await context.SaveChangesAsync(cancellation);

            return new Ok<DeleteToolsIoan>(new DeleteToolsIoan());
        }
   }

   public record DeleteToolsIoan();
}
