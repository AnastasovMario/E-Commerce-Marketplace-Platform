using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository repo;
		public OrderService(IRepository _repo)
		{
			repo = _repo;
		}

		public async Task<IEnumerable<OrderItemViewModel>> GetOrderItems(string userId)
		{
			return await repo.AllReadonly<Item>()
				.Where(i => i.Order.User_Id == userId)
				.OrderByDescending(i => i.Order_Id)
				.Select(i => new OrderItemViewModel
				{
					Id = i.Id,
					Name = i.Product.Name,
					Quantity = i.Quantity,
					Price = i.Product.Price,
					Total = i.Product.Price * i.Quantity,
					ImageUrl = i.Product.ImageUrl,
					Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName,
				})
				.ToListAsync();
		}
    }
}
