using Microsoft.EntityFrameworkCore;
using NewSystem.Data;
using NewSystem.Domain.ToolsIoan;

using MediatR;

namespace NewSystem.App.ToolsIoan
{
   public record UpdateToolsIoanCommand(int ToolIoanId, string ClientName, int TotalItems,
       string ToolsLoanDetails, string ToolsIoanStatus, bool isActive, string Comments) : IRequest<Result<UpdateToolsIoan>>;

    internal class UpdateToolsIoanHandler(NewSystemContext context) : IRequestHandler<UpdateToolsIoanCommand, Result<UpdateToolsIoan>>
    {
        /// <summary>Handles the specified command.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the object if the update was successful.</returns>
        public async Task<Result<UpdateToolsIoan>> Handle(UpdateToolsIoanCommand command, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(command.ClientName) || string.IsNullOrWhiteSpace(command.ToolsLoanDetails) || (command?.TotalItems != null && command.TotalItems <= 0))
                return new Error<UpdateToolsIoan>("ValidateData", "Please check the updated fields again.");

            if (command.TotalItems <= 0)
                return new Error<UpdateToolsIoan>("InvalIdQuantity", "The amount must not be less than 0.");

            ToolsIoans? data = await context.ToolsIoan.FirstOrDefaultAsync(x => x.ToolIoanId == command.ToolIoanId);

            if (data is null)
                return new Error<UpdateToolsIoan>("NotFound", "The record does not exist");

            if (!data.IsActive)
                return new Error<UpdateToolsIoan>("InactiveIoan", "The loan is inactive and cannot be edited.");

            data.Update(command.ClientName, command.TotalItems, command.ToolsLoanDetails, command.ToolsIoanStatus, command.isActive, command.Comments);

            await context.SaveChangesAsync(cancellation);

            return new Ok<UpdateToolsIoan>(new UpdateToolsIoan());
        }
    }

    public record UpdateToolsIoan();
}
