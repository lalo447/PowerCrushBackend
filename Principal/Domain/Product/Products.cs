using NewSystem.Domain.PowerCrushPlayer;

namespace NewSystem.Domain.PowerCrushProduct
{
    /// <summary>
    ///  Enity for products
    /// </summary>
    public class Products
    {
        public string Code { get; private set; }
        public int CategoryId { get; private set; }
        public bool IsComposed { get; private set; }
        public string ImageUrl { get; private set; }

        public void Update(int CategoryId, bool IsComposed, string ImageUrl)
        {
            this.CategoryId = CategoryId;
            this.IsComposed = IsComposed;
            this.ImageUrl = ImageUrl;
        }

        public static Products? Create(string Code, int CategoryId, bool IsComposed, string ImageUrl)
        {
            return new Products
            {
                Code = Code,
                CategoryId = CategoryId,
                IsComposed = IsComposed,
                ImageUrl = ImageUrl
            };
        }
    }
}
