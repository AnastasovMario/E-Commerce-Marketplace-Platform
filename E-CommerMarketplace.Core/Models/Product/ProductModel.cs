using E_CommerceMarketplace.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Product
{
	public class ProductModel : IProductModel
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		[Display(Name = "Product Name")]
		public string Name { get; init; } = null!;

		[Required]
		[Display(Name = "Price")]
		[Precision(18, 2)]
		[Range(0.00, 10000.00, ErrorMessage = "Price must be a positive number and less than {2} leva")]
		public decimal Price { get; init; }

		[Required]
		[StringLength(200)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; init; }

		[MaxLength(2000)]
		[Display(Name = "Description")]
		public string? Description { get; set; }

		public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
	}
}
