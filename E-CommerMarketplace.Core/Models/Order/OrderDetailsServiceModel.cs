using E_CommerceMarketplace.Core.Models.Item;

namespace E_CommerceMarketplace.Core.Models.Order
{
    public class OrderDetailsServiceModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DateCompleted { get; set; }

        public int StatusId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
