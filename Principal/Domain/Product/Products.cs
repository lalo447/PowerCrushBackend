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

    }
}
