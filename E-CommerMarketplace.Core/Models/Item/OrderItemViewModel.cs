namespace E_CommerceMarketplace.Core.Models.Item
{
	public class OrderItemViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

		public string Vendor { get; set; }

		public string ImageUrl { get; set; }

        public bool IsSold { get; set; }
    }
}
