using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IOrderService
	{
		Task<IEnumerable<OrderItemViewModel>> GetOrderItems(int orderId);

		Task<int> GetOrderId(string userId);
	}
}
