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
		[Display(Name = "Price per month")]
		[Precision(18, 2)]
		public decimal Price { get; init; }

		[Required]
		[StringLength(200)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; init; }

		[MaxLength(2000)]
		[Display(Name = "Category")]
		public string? Description { get; set; }

		public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
	}
}
