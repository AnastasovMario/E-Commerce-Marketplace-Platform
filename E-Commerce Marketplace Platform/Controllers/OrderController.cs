using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Commerce_Marketplace_Platform.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ILogger logger;
        public OrderController(IOrderService _orderService,
            ILogger<OrderController> _logger)
        {
            orderService = _orderService;
            logger = _logger;
        }

        public async Task<IActionResult> Mine()
        {
            var model = new OrderViewModel();
            int orderId = await orderService.GetOrderId(User.Id());


            IEnumerable<OrderItemViewModel> orderItems = await orderService.GetOrderItems(orderId);
            model.Id = orderId;
            model.OrderItems = orderItems;
            model.Total = orderItems.Sum(i => i.Total);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int Id)
        {
            if ((await orderService.Exists(Id)) == false)
            {
                ModelState.AddModelError("", "The current order does not exist.");
                logger.LogInformation($"User {User.Id()} attempted to pay for order that doesn't exists.");

                RedirectToAction(nameof(ProductController.All), "Product");
            }

            if ((await orderService.HasUserWithId(Id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to pay for an order [{Id}] that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await orderService.AreAllItemsAvailable(Id)) == false)
            {
                ModelState.AddModelError("", "Not all items are currently available.");
                logger.LogInformation($"Order [{Id}] contains items that are currently unavailable.");

                RedirectToAction(nameof(ProductController.All), "Product");
            }
            await orderService.PayOrder(Id);

            return RedirectToAction(nameof(ProductController.All), "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Clear(int Id)
        {
            if ((await orderService.Exists(Id)) == false)
            {
                ModelState.AddModelError("", "The current order does not exist.");
                logger.LogInformation($"User {User.Id()} attempted to clearn an order that doesn't exists.");

                RedirectToAction(nameof(ProductController.All), "Product");
            }

            if ((await orderService.HasUserWithId(Id, User.Id())) == false)
            {
                logger.LogInformation($"User with id [{User.Id()}] attempted to clear an order [{Id}] that is not his.");

                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            await orderService.ClearOrder(Id);
            return RedirectToAction(nameof(ProductController.All), "Product");
        }
    }
}
