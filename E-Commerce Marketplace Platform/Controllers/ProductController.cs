using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce_Marketplace_Platform.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IVendorService vendorService;
        public ProductController(IProductService _productService,
            IVendorService _vendorService,
            IRepository _repo)
        {
            productService = _productService;
            vendorService = _vendorService;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await productService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Index");
            }

            if ((await productService.HasVendorWithId(id, this.User.Id())) == false)
            {

                return RedirectToPage("/Account/AccessDenied");
            }

            var product = await productService.GetProductDetailsById(id);
            var categoryId = await productService.GetProductCategoryId(id);
            var statusId = await productService.GetProductStatusId(id);

            var model = new ProductEditModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = categoryId,
                StatusId = statusId,
                ProductCategories = await productService.AllCategories(),
                ProductStatuses = await productService.AllProductStatuses()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditModel model)
        {
            if ((await productService.Exists(model.Id)) == false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Index");
            }

            if ((await productService.HasVendorWithId(model.Id, this.User.Id())) == false)
            {

                return RedirectToPage("/Account/AccessDenied");
            }

            if ((await productService.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
            }

            if ((await productService.StatusExists(model.StatusId)) == false)
            {
                ModelState.AddModelError(nameof(model.StatusId), "Status does not exists");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var productId = await productService.Edit(model.Id, model);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<ProductServiceModel> myProducts;
            var userId = User.Id();
            if (await vendorService.ExistsById(userId))
            {
                var vendorId = await vendorService.GetVendorId(userId);
                myProducts = await productService.GetProductsByVendorId(vendorId);
            }
            else
            {
                myProducts = await productService.GetProductsByUserId(userId);
            }

            return View(myProducts);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            if ((await productService.Exists(Id) == false))
            {
                return RedirectToAction(nameof(HomeController.Index), "Index");
            }

            var model = await productService.GetProductDetailsById(Id);

            return View(model);
        }
    }
}
