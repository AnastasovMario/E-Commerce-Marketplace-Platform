using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
    public class ItemService : IItemService
	{
		private readonly IRepository repo;
		public ItemService(IRepository _repo)
		{
			repo = _repo;
		}

        public async Task<int> Create(ItemServiceModel model, string userId)
		{
            var order = new Order()
            {
                Date = DateTime.UtcNow,
                Status_Id = 2,
                User_Id = userId
            };
            order.Items.Add(new()
            {
                Quantity = model.Quantity,
                Product_Id = model.Product_Id,
                Total = Math.Round(model.Quantity * model.Price),
                Order = order
            });

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            return order.Id;
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
                    Total = i.Quantity * i.Product.Price
                })
                .FirstAsync();
        }

        public async Task Remove(int itemId)
        {
            await repo.DeleteAsync<Item>(itemId);
            await repo.SaveChangesAsync();
        }
    }
}
