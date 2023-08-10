using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Product;

namespace E_Commerce_Marketplace_Platform.Areas.Admin.Models
{
	public class MyProductsViewModel
	{
		public IEnumerable<ProductServiceModel> AddedProducts { get; set; }
			= new List<ProductServiceModel>();

		public IEnumerable<ItemServiceModel> BoughtItems { get; set; }
			= new List<ItemServiceModel>();
	}
}
