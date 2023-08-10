using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
    public class ItemService : IItemService
	{
		private readonly IRepository repo;
        private readonly IOrderService orderService;
		public ItemService(IRepository _repo,
            IOrderService _orderService)
		{
			repo = _repo;
            orderService = _orderService;
		}

        public async Task Create(ItemServiceModel model, string userId)
		{
            var order = await orderService.GetCurrentOrderForUser(userId);

            if (order == null)
            {
                var newOrderId = await orderService.CreateOrder(userId);
                var newOrder = await orderService.GetOrderDetails(newOrderId);

                order = newOrder;
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

        public async Task<int> Edit(int itemId, ItemServiceModel model)
        {
            var item = await repo.GetByIdAsync<Item>(itemId);

            item.Quantity = model.Quantity;

            await repo.SaveChangesAsync();

            return item.Id;
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

		public async Task Remove(int itemId)
        {
            await repo.DeleteAsync<Item>(itemId);
            await repo.SaveChangesAsync();
        }
    }
}
