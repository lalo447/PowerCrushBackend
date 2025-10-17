using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;

namespace NewSystem.App.Product
{
    public record GetRandomProductsQuery() : IRequest<Result<List<GetProductsRandomList>>>;

    internal class GetProductsRandomHandle(NewSystemContext context) : IRequestHandler<GetRandomProductsQuery, Result<List<GetProductsRandomList>>>
    {
        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the list of products.</returns>
        public async Task<Result<List<GetProductsRandomList>>> Handle(GetRandomProductsQuery query, CancellationToken cancellationToken)
        {
            var data = await context.Products.OrderBy(_ => EF.Functions.Random())
            .Select(x => new GetProductsRandomList(
                x.Code,
                x.CategoryId,
                x.IsComposed,
                x.ImageUrl
             ))
            .Take(5)
            .ToListAsync();

            if (data is null)
                return new Error<List<GetProductsRandomList>>("NotFound", "Not products were found");

            return new Ok<List<GetProductsRandomList>>(data);
        }
    }

    public record GetProductsRandomList(string Code, int CategoryId, bool IsComposed, string ImageUrl);
}
