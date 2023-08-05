namespace E_CommerceMarketplace.Core.Models.Item
{
	public class ItemServiceModel
	{
		public int Id { get; set; }

		public int Product_Id { get; set; }

		public string Name { get; set; }

		public string Vendor { get; set; }

		public string ImageUrl { get; set; }

		public decimal Price { get; set; }

		public int Quantity { get; set; }
	}
}
