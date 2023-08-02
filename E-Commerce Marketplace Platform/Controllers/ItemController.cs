using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Controllers
{
	[Authorize]
	public class ItemController : Controller
	{
		private readonly IProductService productService;
		private readonly IVendorService vendorService;
		private readonly IItemService itemService;

		public ItemController(IProductService _productService,
			IVendorService _vendorService,
            IItemService _itemService)
		{
			productService = _productService;
			vendorService = _vendorService;
			itemService = _itemService;

        }

		[HttpGet]
		public async Task<IActionResult> Buy(int id)
		{

			var product = await productService.GetProductDetailsById(id);
			var itemModel = new ItemConfirmationModel()
			{
				ImageUrl = product.ImageUrl,
				Product_Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Vendor = product.Vendor.FirstName + " " + product.Vendor.LastName,
			};
			return View(itemModel);
		}

		[HttpPost]
		public async Task<IActionResult> Buy(ItemConfirmationModel model)
		{
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var orderId = await itemService.Create(model, User.Id());

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}
	}
}
