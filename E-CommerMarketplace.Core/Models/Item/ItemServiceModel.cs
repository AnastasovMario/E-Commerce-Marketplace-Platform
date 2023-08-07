using E_CommerceMarketplace.Core.Constants;
using System.ComponentModel.DataAnnotations;

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

		[Display(Name = "Quantity")]
		[Range(ValidationConstants.MinimumQuantity, ValidationConstants.MaximumQuantity, ErrorMessage = "Quantity must be a positive number and less than 10")]
		public int Quantity { get; set; }
	}
}
