using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Models.Order
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public decimal Total { get; set; }
		public List<OrderItemViewModel> OrderItems { get; set; }
	}
}
