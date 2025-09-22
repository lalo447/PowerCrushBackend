using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;

namespace NewSystem.App.Product
{
   public record GetProductsQuery() : IRequest<Result<List<GetProductsList>>>;

    internal class GetProductsHandle(NewSystemContext context) : IRequestHandler<GetProductsQuery, Result<List<GetProductsList>>>
    {
        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the list of products.</returns>
        public async Task<Result<List<GetProductsList>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var data = await context.Products
            .Select(x => new GetProductsList(
                x.Code,
                x.CategoryId,
                x.IsComposed,
                x.ImageUrl
             ))
            .ToListAsync();

            if (data is null)
                return new Error<List<GetProductsList>>("NotFound", "Not products were found");

            return new Ok<List<GetProductsList>>(data);
        }
    }

    public record GetProductsList(string Code, int CategoryId, bool IsComposed, string ImageUrl);
}
