using E_Commerce_Marketplace_Platform.Extensions;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
			IEnumerable<OrderItemViewModel> model;

			model = await orderService.GetOrderItems(User.Id());

			return View(model);
		}
	}
}
