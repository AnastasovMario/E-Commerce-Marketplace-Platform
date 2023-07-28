using E_Commerce_Marketplace_Platform.Models;
using E_CommerceMarketplace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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