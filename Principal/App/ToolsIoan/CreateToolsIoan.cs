using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.App.ToolsIoan
{
   public record CreateToolsIoanCommand(string ClientName, int TotalItems, 
       string ToolsLoanDetails, string ToolsIoanStatus) : IRequest<Result<CreateToolsIoan>>;
   
   internal class CreateToolsIoanHandler(NewSystemContext context, ICreateNumberIoan create) : IRequestHandler<CreateToolsIoanCommand, Result<CreateToolsIoan>>
    {
        /// <summary>Handles the specified command.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the CreateToolsIoan object if the insertion was successful.</returns>
        public async Task<Result<CreateToolsIoan>> Handle(CreateToolsIoanCommand command, CancellationToken cancellation)
        {
            if (command.TotalItems <= 0)
                return new Error<CreateToolsIoan>("InvalIdQuantity", "The amount must not be less than 0.");

            int numberIaon = 0;
            ToolsIoans? validateNumberIoan = null;

            do
            {
                numberIaon = await create.GenerateNumberIoan();
                validateNumberIoan = await context.ToolsIoan.FirstOrDefaultAsync(x => x.NumberIoan == numberIaon && x.IsActive);
            } while (validateNumberIoan is not null);

            ToolsIoans? entity = ToolsIoans.Create(command.ClientName, command.TotalItems,
                command.ToolsLoanDetails, command.ToolsIoanStatus, numberIaon);

            if (entity is null)
                return new Error<CreateToolsIoan>("NotFound", "There was an error in the insertion.");

            context.ToolsIoan.Add(entity);
            await context.SaveChangesAsync(cancellation);

            return new Ok<CreateToolsIoan>(new CreateToolsIoan());
        }
    }

   public record CreateToolsIoan();
}
