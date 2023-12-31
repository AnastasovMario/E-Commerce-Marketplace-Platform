﻿using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_CommerceMarketplace.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;
        private readonly ILogger<OrderService> logger;
        public OrderService(IRepository _repo, ILogger<OrderService> _logger)
        {
            repo = _repo;
            logger = _logger;
        }

        public async Task<bool> AreAllItemsAvailable(int orderId)
        {
            var orderItems = await GetOrderItems(orderId);

            var orderItemsProductIds = orderItems
                .Select(i => i.Product_Id)
                .ToList();

            bool anyUnavailable = await repo.AllReadonly<Product>()
                .Where(p => orderItemsProductIds.Contains(p.Id) && p.Status_Id == Status.Unavailable.Id)
                .AnyAsync();

            return !anyUnavailable;
        }

        public async Task ClearOrder(int orderId)
        {
            try
            {
                var orderItems = await repo.AllReadonly<Item>()
                    .Where(i => i.Order_Id == orderId)
                    .AsNoTracking() 
                    .ToListAsync();

                var order = await repo.GetByIdAsync<Order>(orderId);

                foreach (var item in orderItems)
                {
                    await repo.DeleteAsync<Item>(item.Id);
                }

                await repo.DeleteAsync<Order>(orderId);

                await repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(ClearOrder), ex);
                throw new ApplicationException($"Database failed to clear order [{orderId}]", ex);
            }
        }

        public async Task<int> CreateOrder(string userId)
        {
            try
            {
                var order = new Order()
                {
                    Date = DateTime.UtcNow,
                    User_Id = userId
                };

                await repo.AddAsync(order);
                await repo.SaveChangesAsync();

                return order.Id;
            }
            catch (Exception ex)
            {

                logger.LogError(nameof(CreateOrder), ex);
                throw new ApplicationException($"Database failed to create order", ex);
            }
            
        }

        public async Task<bool> Exists(int orderId)
        {
            return await repo.AllReadonly<Order>()
                .AnyAsync(o => o.Id == orderId);
        }

        public async Task<OrderDetailsServiceModel> GetCurrentOrderForUser(string userId)
        {
            return await repo.AllReadonly<Order>()
                .Where(o => o.User_Id == userId && o.Sale_Id == null)
                .Select(o => new OrderDetailsServiceModel()
                {
                    Id = o.Id,
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
                    Product_Id = i.Product_Id,
                    Name = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Product.Price,
                    Total = i.Product.Price * i.Quantity,
                    ImageUrl = i.Product.ImageUrl,
                    Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName,
                })
                .ToListAsync();
        }

        public async Task<bool> HasUserWithId(int orderId, string userId)
        {
            return await repo.AllReadonly<Order>()
                .AnyAsync(o => o.Id == orderId && o.User_Id == userId);
        }

        public async Task PayOrder(int orderId)
        {
            try
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
                order.Sale = sale;

                await repo.AddAsync(sale);
                await repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                logger.LogError(nameof(ClearOrder), ex);
                throw new ApplicationException($"Database failed to pay for order [{orderId}]", ex);
            }
        }
    }
}
