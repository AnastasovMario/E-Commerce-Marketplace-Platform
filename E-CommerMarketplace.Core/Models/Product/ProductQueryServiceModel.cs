namespace E_CommerceMarketplace.Core.Models.Product
{
    public class ProductQueryServiceModel
    {
        public int TotalProductsCount { get; set; }

        public List<ProductServiceModel> Products { get; set; }
        = new List<ProductServiceModel>();
    }
}
