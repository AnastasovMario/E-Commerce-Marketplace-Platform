using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IOrderService
	{
		Task<bool> Exists(int orderId);

		Task<bool> HasUserWithId(int orderId, string userId);

		Task<bool> AreAllItemsAvailable(int orderId);

		Task<IEnumerable<OrderItemViewModel>> GetOrderItems(int orderId);

		Task<int> GetOrderId(string userId);

		Task<OrderDetailsServiceModel> GetCurrentOrderForUser(string userId);

		Task<OrderDetailsServiceModel> GetOrderDetails(int orderId);

		Task<int> CreateOrder(string userId);

		Task PayOrder(int orderId);

		Task ClearOrder(int orderId);
	}
}
