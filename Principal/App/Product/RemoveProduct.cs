using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;

namespace NewSystem.App.Product
{
    public record RemoveProductCommand(string Code) : IRequest<Result<bool>>;
    internal class RemoveProduct(NewSystemContext context) : IRequestHandler<RemoveProductCommand, Result<bool>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<Result<bool>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var remove = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Code == request.Code);

            if (remove is null)
                return new Error<bool>("NotFound", "The record to be deleted was not found");

            context.Remove(remove);

            await context.SaveChangesAsync();

            return new Ok<bool>(true);
        }
    }
}
