using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace E_Commerce_Marketplace_Platform.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService productService;
		private readonly IVendorService vendorService;
		public ProductController(IProductService _productService,
			IVendorService _vendorService)
		{
			productService = _productService;
			vendorService= _vendorService;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			if ((await vendorService.ExistsById(User.Id()) == false))
			{
				return RedirectToAction(nameof(VendorController.Become), "Vendor");
			}

			var model = new ProductModel()
			{
				ProductCategories = await productService.AllCategories()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ProductModel model)
		{
			if ((await vendorService.ExistsById(User.Id()) == false))
			{
				return RedirectToAction(nameof(VendorController.Become), "Vendor");
			}

			if ((await productService.CategoryExists(model.CategoryId)) == false)
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var vendorId = await vendorService.GetVendorId(User.Id());

			var id = await productService.Create(model, vendorId);

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}
	}
}
