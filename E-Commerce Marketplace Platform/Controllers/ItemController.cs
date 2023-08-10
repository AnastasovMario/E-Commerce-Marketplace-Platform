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
                logger.LogInformation($"User with id [{User.Id()}] attempted to buy his own product [{model.Product_Id}]");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await productService.IsProductAvailable(model.Id)) == false)
            {
                logger.LogInformation($"User {User.Id()} attempted to buy a product [{model.Product_Id}] that is not available");

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
            if ((await itemService.Exists(id)) == false)
            {
                logger.LogInformation($"User {User.Id()} attempted to edit an item that doesn't exist.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

            if ((await itemService.HasBuyerWithId(id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to edit an item [{id}] that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await itemService.IsItemBought(id))
            {
                logger.LogInformation($"User {User.Id()} attempted to edit an item [{id}] that is already bought.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }
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
            if (Id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await itemService.Exists(Id)) == false)
            {
                ModelState.AddModelError("", "Item does not exist");

                logger.LogInformation($"User {User.Id()} attempted to edit an item that doesn't exist.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

            if ((await itemService.HasBuyerWithId(Id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to edit an item [{Id}] that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await itemService.IsItemBought(Id))
            {
                ModelState.AddModelError("", "Item is already bought.");

                logger.LogInformation($"User {User.Id()} attempted to edit an item [{Id}] that is already bought.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

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
            if ((await itemService.Exists(Id)) == false)
            {
                ModelState.AddModelError("", "Item does not exist");

                logger.LogInformation($"User {User.Id()} attempted to remove an item that doesn't exist.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

            if ((await itemService.HasBuyerWithId(Id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to remove an item [{Id}]that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await itemService.IsItemBought(Id))
            {
                ModelState.AddModelError("", "Item is already bought.");

                logger.LogInformation($"User {User.Id()} attempted to remove an item [{Id}] that is already bought.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

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
            if ((await itemService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Item does not exist");

                logger.LogInformation($"User {User.Id()} attempted to remove an item that doesn't exist.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

            if ((await itemService.HasBuyerWithId(model.Id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to remove an item [{model.Id}] that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await itemService.IsItemBought(model.Id))
            {
                ModelState.AddModelError("", "Item is already bought and cannot be removed.");

                logger.LogInformation($"User {User.Id()} attempted to remove an item [{model.Id}] that is already bought.");

                RedirectToAction(nameof(OrderController.Mine), "Order");
            }

            await itemService.Remove(model.Id);

            return RedirectToAction(nameof(OrderController.Mine), "Order");
        }

		public async Task<IActionResult> History()
		{
			var model = await itemService.GetItemsHistory(User.Id());

			return View(model);
		}
    }
}
