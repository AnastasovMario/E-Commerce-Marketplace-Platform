using E_CommerceMarketplace.Core.Constants;
using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Product
{
	public class ProductModel : IProductModel
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(DbConstants.ProductNameLength)]
		[Display(Name = "Product Name")]
		public string Name { get; set; } = null!;

		[Required]
		[Display(Name = "Price")]
		[Precision(18, 2)]
		[Range(ValidationConstants.MinimumPrice, ValidationConstants.MaximumPrice, ErrorMessage = "Price must be a positive number and less than {2} leva")]
		public decimal Price { get; init; }

		[Required]
		[StringLength(DbConstants.ImageUrlMaxLength)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; init; }

		[MaxLength(DbConstants.DescriptionLength)]
		[Display(Name = "Description")]
		public string? Description { get; set; }

		public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
	}
}
