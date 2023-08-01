namespace E_CommerceMarketplace.Core.Models.Product
{
	public class OrderServiceModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public decimal Price { get; set; }

		public string Vendor { get; set; } = null!;
	}
}
