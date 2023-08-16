using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_CommerceMarketplace.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository repo;
        private readonly IOrderService orderService;
        private readonly ILogger logger;
        public ItemService(IRepository _repo,
            IOrderService _orderService,
            ILogger<ItemService> _logger)
        {
            repo = _repo;
            orderService = _orderService;
            logger = _logger;
        }

        public async Task Create(ItemServiceModel model, string userId)
        {

            try
            {
                var order = await orderService.GetCurrentOrderForUser(userId);

                if (order == null)
                {
                    var newOrderId = await orderService.CreateOrder(userId);
                    var newOrder = await orderService.GetOrderDetails(newOrderId);

                    order = newOrder;
                }

                if (model.Quantity < 1)
                {
                    throw new ArgumentException($"Quantity cannot be less than 1.");
                }

                var item = new Item()
                {
                    Quantity = model.Quantity,
                    Product_Id = model.Product_Id,
                    Total = Math.Round(model.Quantity * model.Price),
                    Order_Id = order.Id
                };

                await repo.AddAsync(item);
                await repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(Create), ex);
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> Edit(int itemId, ItemServiceModel model)
        {
            try
            {
                var item = await repo.GetByIdAsync<Item>(itemId);

                if (model.Quantity < 1)
                {
                    throw new ArgumentException($"Quantity cannot be less than 1.");
                }

                item.Quantity = model.Quantity;

                await repo.SaveChangesAsync();

                return item.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(Edit), ex);
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<bool> Exists(int itemId)
        {
            return await repo.AllReadonly<Item>()
                .AnyAsync(i => i.Id == itemId);
        }

        public async Task<OrderItemViewModel> GetItemById(int itemId)
        {
            return await repo.AllReadonly<Item>()
                .Where(i => i.Id == itemId)
                .Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    Name = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Total = i.Quantity * i.Product.Price,
                    Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName
                })
                .FirstAsync();
        }

        public async Task<IEnumerable<OrderItemViewModel>> GetItemsHistory(string userId)
        {
            var orderIds = await repo.AllReadonly<Order>()
            .Where(o => o.User_Id == userId && o.Sale_Id != null)
            .Select(o => o.Id)
            .ToListAsync();

            return await repo.AllReadonly<Item>()
                .Where(i => orderIds.Contains(i.Order_Id))
                .Select(i => new OrderItemViewModel
                {
                    Id = i.Id,
                    Name = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Total = i.Quantity * i.Product.Price,
                    IsSold = true,
                    Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ItemServiceModel>> GetUsersBoughtProducts(string userId)
        {
            return await repo.AllReadonly<Item>()
                .Where(i => i.Order.User_Id == userId && i.Order.Sale_Id != null)
                .Select(i => new ItemServiceModel
                {
                    Name = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Price = i.Product.Price,
                    Quantity = i.Quantity,
                    Vendor = i.Product.Vendor.FirstName + " " + i.Product.Vendor.LastName
                })
                .ToListAsync();
        }

        public async Task<bool> HasBuyerWithId(int itemId, string userId)
        {
            return await repo.AllReadonly<Item>()
                .AnyAsync(i => i.Id == itemId && i.Order.User_Id == userId);
        }

        public async Task<bool> IsItemBought(int itemId)
        {
            return await repo.AllReadonly<Item>()
                .AnyAsync(i => i.Id == itemId && i.Order.Sale_Id != null);
        }

        public async Task Remove(int itemId)
        {
            try
            {
                await repo.DeleteAsync<Item>(itemId);
                await repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(Remove), ex);
                throw new ApplicationException("Database failed to remove item", ex);
            } 
        }
    }
}
