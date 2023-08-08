using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Product;

namespace E_Commerce_Marketplace_Platform.Areas.Admin.Models
{
    public class MyProductsViewModel
    {
        public IEnumerable<ItemServiceModel> PurchasedItems { get; set; } = new List<ItemServiceModel>();

        public IEnumerable<ProductServiceModel> MyProducts { get; set; } = new List<ProductServiceModel>();
    }
}
