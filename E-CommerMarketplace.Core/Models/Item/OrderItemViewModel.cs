namespace E_CommerceMarketplace.Core.Models.Item
{
	public class OrderItemViewModel
	{
        public int Product_Id { get; set; }

        public int Id { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

		public string Vendor { get; set; }

		public string ImageUrl { get; set; }
    }
}
