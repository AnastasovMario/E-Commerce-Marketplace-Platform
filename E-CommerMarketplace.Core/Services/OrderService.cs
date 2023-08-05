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

        public async Task<int> CreateOrder(string userId)
        {
            var order = new Order()
            {
                Date = DateTime.UtcNow,
                Status_Id = 2,
                User_Id = userId
            };

            //order.Items.Add(new()
            //{
            //    Quantity = model.Quantity,
            //    Product_Id = model.Product_Id,
            //    Total = Math.Round(model.Quantity * model.Price),
            //    Order = order
            //});

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderDetailsServiceModel> GetCurrentOrderForUser(string userId)
        {
            return await repo.AllReadonly<Order>()
                .Where(o => o.User_Id == userId)
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
                .Where(i => i.Order_Id == orderId)
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
