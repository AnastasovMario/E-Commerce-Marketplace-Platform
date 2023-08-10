using E_Commerce_Marketplace_Platform.Areas.Admin.Models;
using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Areas.Admin.Controllers
{
	public class ProductController : BaseController
	{
		private readonly IProductService productService;

		private readonly IItemService itemService;

        public ProductController(IProductService _productService,
			IItemService _itemService)
        {
            productService = _productService;
			itemService = _itemService;
        }
        public async Task<IActionResult> Mine()
		{
			var myProducts = new MyProductsViewModel();
			var adminId = User.Id();
			myProducts.AddedProducts = await productService.GetUserProducts(adminId);
			myProducts.BoughtItems = await itemService.GetUsersBoughtProducts(adminId);

			return View(myProducts);
		}
	}
}
