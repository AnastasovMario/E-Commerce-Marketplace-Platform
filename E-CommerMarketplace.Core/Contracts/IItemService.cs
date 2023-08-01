using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IItemService
	{
		Task<int> Create(ItemConfirmationModel model, int orderId);
	}
}
