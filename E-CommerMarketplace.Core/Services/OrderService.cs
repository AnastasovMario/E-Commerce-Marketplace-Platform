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

		public async Task ClearOrder(int orderId)
		{
            var orderItems = await repo.AllReadonly<Item>()
                .Where(i => i.Order_Id == orderId)
                .ToListAsync();
            var order = await repo.GetByIdAsync<Order>(orderId);

            foreach (var item in orderItems)
            {
                repo.Delete(item);
            }

            repo.Delete(order);

            await repo.SaveChangesAsync();
		}

		public async Task<int> CreateOrder(string userId)
        {
            var order = new Order()
            {
                Date = DateTime.UtcNow,
                Status_Id = 2,
                User_Id = userId
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderDetailsServiceModel> GetCurrentOrderForUser(string userId)
        {
            return await repo.AllReadonly<Order>()
                .Where(o => o.User_Id == userId && o.Sale_Id == null)
                .Select(o => new OrderDetailsServiceModel()
                {
                    Id = o.Id,
                    StatusId = o.Status_Id,
                    Date = o.Date,
                    DateCompleted = o.DateCompleted,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetailsServiceModel> GetOrderDetails(int orderId)
        {
            return await repo.AllReadonly<Order>()
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDetailsServiceModel
                {
                    Id = o.Id,
                    StatusId = o.Status_Id,
                    Date = o.Date,
                    DateCompleted = o.DateCompleted
                })
                .FirstAsync();
        }

        public async Task<int> GetOrderId(string userId)
        {
            return await repo.AllReadonly<Order>()
                .Where(o => o.User_Id == userId)
                .OrderByDescending(o => o.Id)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderItemViewModel>> GetOrderItems(int orderId)
        {
            return await repo.AllReadonly<Item>()
                .Where(i => i.Order_Id == orderId && i.Order.Sale_Id == null)
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

        public async Task PayOrder(int orderId)
        {       
            var order = await repo.GetByIdAsync<Order>(orderId);

            var itemsTotal = await repo.AllReadonly<Item>()
                .Where(i => i.Order_Id == order.Id)
                .SumAsync(i => i.Total);

            var sale = new Sale()
            {
                Total = itemsTotal,
                Date = DateTime.UtcNow,
                Buyer_Id = order.User_Id
            };

			order.DateCompleted = DateTime.UtcNow;
			order.Status_Id = 1;
            order.Sale = sale;

			await repo.AddAsync(sale);
            await repo.SaveChangesAsync();
        }
    }
}
