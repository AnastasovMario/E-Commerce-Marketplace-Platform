using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IItemService
	{
		Task<bool> HasBuyerWithId(int itemId, string userId);

		Task<bool> Exists(int itemId);

		Task<bool> IsItemBought(int itemId);

		Task<IEnumerable<OrderItemViewModel>> GetUsersBoughtProducts(string userId);

		Task Create(ItemServiceModel model, string userId);

		Task Remove(int itemId);

		Task<OrderItemViewModel> GetItemById(int itemId);

		Task<int> Edit(int itemId, ItemServiceModel model);

		Task<IEnumerable<OrderItemViewModel>> GetItemsHistory(string userId);
	}
}
