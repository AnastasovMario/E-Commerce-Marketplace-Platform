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
		private readonly IItemService itemService;
		private readonly ILogger logger;

		public ItemController(IProductService _productService,
            IItemService _itemService,
            ILogger<ItemController> _logger)
		{
			productService = _productService;
			itemService = _itemService;
			logger = _logger;
        }

		[HttpGet]
		public async Task<IActionResult> Buy(int id)
		{
			if ((await productService.Exists(id)) == false)
			{
				logger.LogInformation($"User {User.Id()} attempted to buy a product that doesn't exists");

				RedirectToAction(nameof(ProductController.All), "Product");
			}

			if (await productService.HasVendorWithId(id, User.Id()))
			{
                logger.LogInformation($"User with id [{User.Id()}] attempted to buy his own product [{id}]");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

			if ((await productService.IsProductAvailable(id)) == false)
			{
                logger.LogInformation($"User {User.Id()} attempted to buy a product [{id}] that is not avaiable");

                RedirectToAction(nameof(ProductController.All), "Product");
            }

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
            if ((await productService.Exists(model.Id)) == false)
            {
                logger.LogInformation($"User {User.Id()} attempted to buy a product that doesn't exist.");

                RedirectToAction(nameof(ProductController.All), "Product");
            }

            if (await productService.HasVendorWithId(model.Id, User.Id()))
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to buy his own product [{model.Id}]");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await productService.IsProductAvailable(model.Id)) == false)
            {
                logger.LogInformation($"User {User.Id()} attempted to buy a product [{model.Id}] that is not avaiable");

                RedirectToAction(nameof(ProductController.All), "Product");
            }

            if (!ModelState.IsValid)
			{
				return View(model);
			}

			await itemService.Create(model, User.Id());

			return RedirectToAction(nameof(OrderController.Mine), "Order");
		}

		[HttpGet]
        public async Task<IActionResult> Edit(int id)
		{
            var item = await itemService.GetItemById(id);

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
