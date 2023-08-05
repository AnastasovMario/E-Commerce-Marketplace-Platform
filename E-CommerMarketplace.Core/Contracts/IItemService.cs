using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IItemService
	{
		Task<int> Create(ItemConfirmationModel model, string userId);

		Task Remove(int itemId);

		Task<OrderItemViewModel> GetItemById(int itemId);
	}
}
