using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
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
		public async Task<IEnumerable<OrderItemViewModel>> GetOrderItems(int orderId, string userId)
		{
			return await repo.AllReadonly<Item>()
				.Where(i => i.Order_Id == orderId && i.Order.User_Id == userId)
				.Select(i => new OrderItemViewModel
				{
					Id = i.Id,
					Name = i.Product.Name,
					Quantity = i.Quantity,
					Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName,
				})
				.ToListAsync();
		}
	}
}
