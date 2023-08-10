using E_Commerce_Marketplace_Platform.Extensions;
using E_Commerce_Marketplace_Platform.Helpers;
using E_Commerce_Marketplace_Platform.Models;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Extensions;
using E_CommerceMarketplace.Core.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Commerce_Marketplace_Platform.Areas.Admin.Constants.AdminConstants;

namespace E_Commerce_Marketplace_Platform.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IVendorService vendorService;
        private readonly ILogger logger;
        private readonly SanitizerHelper sanitizer;
        public ProductController(IProductService _productService,
            IVendorService _vendorService,
            SanitizerHelper _sanitizer,
            ILogger<ProductController> _logger)
        {
            productService = _productService;
            vendorService = _vendorService;
            sanitizer = _sanitizer;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllProductsQueryModel model)
        {
            model.SearchTerm = sanitizer.Sanitize(model.SearchTerm);

            var productsResult = await productService.All(model.Category,
                model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            model.TotalProductsCount = productsResult.TotalProductsCount;
            model.Products = productsResult.Products;

            var productCategories = await productService.AllCategoriesNames();
            var productStatuses = await productService.AllStatusesNames();

            model.Statuses = productStatuses;
            model.Categories = productCategories;

            return View(model);
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
            model.Name = sanitizer.Sanitize(model.Name);
            model.Description = sanitizer.Sanitize(model.Description);
            model.ImageUrl = sanitizer.Sanitize(model.ImageUrl);

            if ((await vendorService.ExistsById(User.Id()) == false))
            {
                return RedirectToAction(nameof(VendorController.Become), "Vendor");
            }

            if ((await productService.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
                model.ProductCategories = await productService.AllCategories();

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.ProductCategories = await productService.AllCategories();
                return View(model);
            }

            var vendorId = await vendorService.GetVendorId(User.Id());

            var id = await productService.Create(model, vendorId);

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await productService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await productService.HasVendorWithId(id, User.Id())) == false)
            {
                logger.LogInformation("User with id {0} attempted to edit other vendor's product", User.Id());

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
            if (id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            model.Name = sanitizer.Sanitize(model.Name);
            model.Description = sanitizer.Sanitize(model.Description);
            model.ImageUrl = sanitizer.Sanitize(model.ImageUrl);

            if ((await productService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Product does not exist");
                model.ProductStatuses = await productService.AllProductStatuses();
                model.ProductCategories = await productService.AllCategories();

                return View(model);
            }

            if ((await productService.HasVendorWithId(model.Id, this.User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await productService.CategoryExists(model.CategoryId)) == false)
            {

                ModelState.AddModelError("", "Category does not exist");
                model.ProductStatuses = await productService.AllProductStatuses();
                model.ProductCategories = await productService.AllCategories();

                return View(model);
            }

            if ((await productService.StatusExists(model.StatusId)) == false)
            {
                ModelState.AddModelError("", "Status does not exists");
                model.ProductStatuses = await productService.AllProductStatuses();
                model.ProductCategories = await productService.AllCategories();

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { id = model.Id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if ((await productService.Exists(Id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await productService.HasVendorWithId(Id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var product = await productService.GetProductDetailsById(Id);

            var model = new ProductDetailsViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductDetailsViewModel model)
        {
            if ((await productService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }
            if ((await productService.HasVendorWithId(model.Id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await productService.Delete(id);

           return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Mine()
        {
            if (User.IsInRole(AdminRolleName))
            {
                return RedirectToAction("Mine", "Product", new { area = AreaName });
            }

            var myProducts = new List<ProductServiceModel>();
            var userId = User.Id();

            if (await vendorService.ExistsById(userId))
            {
                var vendorId = await vendorService.GetVendorId(userId);
                myProducts = await productService.GetProductsByVendorId(vendorId);
            }

            return View(myProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id, string information)
        {
            if ((await productService.Exists(Id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            var model = await productService.GetProductDetailsById(Id);

            if (information != model.GetInformation())
            {
                TempData["ErrorMessage"] = "Don't touch my precious!";

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
