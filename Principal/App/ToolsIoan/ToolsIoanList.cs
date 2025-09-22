using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;

namespace NewSystem.App.ToolsIoan
{
   public record GetToolsQuery(): IRequest<Result<List<ToolsIoanList>>>;

   internal class GetToolsHandler(NewSystemContext context) : IRequestHandler<GetToolsQuery, Result<List<ToolsIoanList>>>
    {
        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>obtains the data of the product loan.</returns>
        public async Task<Result<List<ToolsIoanList>>> Handle(GetToolsQuery query, CancellationToken cancellation)
        {
            var data = await context.ToolsIoan
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedDate)
                .Select(t => new ToolsIoanList(               
                    t.ToolIoanId,
                    t.ClientName ?? "",                    
                    t.NumberIoan,
                    t.TotalItems,
                    t.IsActive,
                    t.CreatedDate,
                    t.ReturnDate ?? null,                            
                    t.ToolsLoanDetails ?? "",
                    t.ToolsIoanStatus ?? "",
                    t.Comments ?? ""
                ))
                .ToListAsync(cancellation);

            if (data is null)
                return new Error<List<ToolsIoanList>>("NotFound", "No data recorded.");

            return new Ok<List<ToolsIoanList>>(data);
        }
    }

    public record ToolsIoanList(int ToolIoanId, string ClientName, int NumberIoan, int TotalItems, bool IsActive,
        DateTime CreatedDate, DateTime? ReturnDate, string ToolsLoanDetails, string ToolsIoanStatus, string Comments);
}
