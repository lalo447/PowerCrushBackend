using MediatR;
using Microsoft.EntityFrameworkCore;
using NewSystem.Data;
using NewSystem.Domain.PowerCrushProduct;

namespace NewSystem.App.Product
{
    public record UpSertProductCommand(string Code, int CategoryId, bool IsComposed, string ImageUrl) : IRequest<Result<bool>>;
    internal class RegisterProduct(NewSystemContext context) : IRequestHandler<UpSertProductCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpSertProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Code == request.Code);

            if (string.IsNullOrEmpty(request.Code) || request.CategoryId == null || request.IsComposed == null || string.IsNullOrEmpty(request.ImageUrl))
                return new Error<bool>("NotFound", "");

            if (product is not null)
            {
                product.Update(request.CategoryId, request.IsComposed, request.ImageUrl);
                await context.SaveChangesAsync();

                return new Ok<bool>(true);
            }

            Products? productRegister = Products.Create(request.Code, request.CategoryId, request.IsComposed, request.ImageUrl);

            context.Add(productRegister);
            await context.SaveChangesAsync();
            return new Ok<bool>(true);
        }
    }
}
