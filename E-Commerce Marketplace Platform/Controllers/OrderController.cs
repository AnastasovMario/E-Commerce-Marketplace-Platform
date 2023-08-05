using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Controllers
{
    [Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
			orderService = _orderService;
        }

		[HttpGet]
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
			await orderService.PayOrder(Id);

            return RedirectToAction(nameof(ProductController.All), "Product");
        }

		[HttpPost]
		public async Task<IActionResult> Clear(int Id)
		{
			await orderService.ClearOrder(Id);
			return RedirectToAction(nameof(ProductController.All), "Product");
		}
	}
}
