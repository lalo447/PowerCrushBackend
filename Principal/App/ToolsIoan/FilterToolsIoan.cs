using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.App.Shared;
using NewSystem.App.ToolsIoan.Filters;
using NewSystem.Data;
using NewSystem.Domain.ToolsIoan;

namespace NewSystem.App.ToolsIoan
{
    public record FilterToolsIoanCommand(
        int? NumberIoan, string? ClientName, bool? IsActive, string? ToolsIoanStatus
    ) : IRequest<Result<List<FilterToolsIoan>>>;

    internal sealed class FilterToolsIoanHandler(NewSystemContext context)
        : IRequestHandler<FilterToolsIoanCommand, Result<List<FilterToolsIoan>>>
    {
        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellation">The ct.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<Result<List<FilterToolsIoan>>> Handle(
            FilterToolsIoanCommand request, CancellationToken cancellation)
        {
            var filters = new List<IFilter<ToolsIoans>>();

            if (!string.IsNullOrWhiteSpace(request.ToolsIoanStatus))
                filters.Add(new ToolsIoanStatusFilter(request.ToolsIoanStatus!));

            if (!string.IsNullOrWhiteSpace(request.ClientName))
                filters.Add(new ClientNameContainsFilter(request.ClientName!));

            if (request.IsActive.HasValue)
                filters.Add(new IsActiveFilter(request.IsActive.Value));

            if (request.NumberIoan.HasValue)
                filters.Add(new NumberIoanEqualsFilter(request.NumberIoan.Value));

            var data = context.ToolsIoan.AsNoTracking().ApplyFilters(filters);

            var rows = await data
                .OrderByDescending(t => t.CreatedDate)
                .Select(t => new FilterToolsIoan(
                    t.ToolIoanId,
                    t.ClientName ?? "",
                    t.NumberIoan,
                    t.TotalItems,
                    t.IsActive,
                    t.CreatedDate,
                    t.ReturnDate,
                    t.ToolsLoanDetails ?? "",
                    t.ToolsIoanStatus ?? "",
                    t.Comments ?? ""))
                .ToListAsync(cancellation);

            if (rows is null)
                return new Error<List<FilterToolsIoan>>("NotFound", "No data recorded.");

            return new Ok<List<FilterToolsIoan>>(rows);
        }
    }
    public record FilterToolsIoan(
        int ToolIoanId, string ClientName, int NumberIoan, int TotalItems, bool IsActive,
        DateTime CreatedDate, DateTime? ReturnDate, string ToolsLoanDetails, string ToolsIoanStatus, string Comments);
}
