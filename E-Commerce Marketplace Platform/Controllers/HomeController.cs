using E_Commerce_Marketplace_Platform.Models;
using E_CommerceMarketplace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static E_Commerce_Marketplace_Platform.Areas.Admin.Constants.AdminConstants;

namespace E_Commerce_Marketplace_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
			if (User.IsInRole(AdminRolleName))
			{
				return RedirectToAction("Index", "Admin", new { area = "Admin" });
			}
			var model = await _productService.GetLastProducts();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}