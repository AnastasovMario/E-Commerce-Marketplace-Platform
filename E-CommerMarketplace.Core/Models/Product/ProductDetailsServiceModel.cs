using E_CommerceMarketplace.Core.Models.Vendor;

namespace E_CommerceMarketplace.Core.Models.Product
{
	public class ProductDetailsServiceModel : ProductServiceModel
	{
		public string? Description { get; init; } 

		public string Category { get; init; } = null!;

		public VendorServiceModel Vendor { get; init; } = null!;
	}
}
