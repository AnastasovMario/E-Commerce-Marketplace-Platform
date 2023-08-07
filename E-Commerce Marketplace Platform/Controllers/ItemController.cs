using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
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
			var itemModel = new ItemServiceModel()
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
		public async Task<IActionResult> Buy(ItemServiceModel model)
		{
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await itemService.Create(model, User.Id());

			return RedirectToAction(nameof(OrderController.Mine), "Order");
		}

		[HttpGet]
        public async Task<IActionResult> Edit(int Id)
		{
			var item = await itemService.GetItemById(Id);

			var model = new ItemServiceModel()
			{
				Id = item.Id,
				Quantity = item.Quantity,
				ImageUrl = item.ImageUrl,
				Name = item.Name,
				Price = item.Price,
				Vendor = item.Vendor
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int Id, ItemServiceModel model)
		{


			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var itemId = await itemService.Edit(Id, model);

			return RedirectToAction(nameof(OrderController.Mine), "Order");
        }


        [HttpGet]
		public async Task<IActionResult> Remove(int Id)
		{
			var item = await itemService.GetItemById(Id);

			var model = new ItemRemoveViewModel()
			{
				Id = item.Id,
				Name = item.Name,
				ImageUrl = item.ImageUrl,
				Total = item.Total
			};


            return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> Remove(ItemRemoveViewModel model)
        {

			await itemService.Remove(model.Id);

            return RedirectToAction(nameof(OrderController.Mine), "Order");
        }

		[HttpGet]
		public async Task<IActionResult> History()
		{
			var model = await itemService.GetItemsHistory(User.Id());

			return View(model);
		}
    }
}
