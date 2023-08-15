using E_CommerceMarketplace.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Core.Models.Product
{
    public class ProductModel : IProductModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
		[Display(Name = "Product Name")]
		public string Name { get; set; } = null!;

		[Required]
		[Display(Name = "Price")]
		[Precision(18, 2)]
		[Range(MinimumPrice, MaximumPrice, ErrorMessage = "Price must be a positive number and less than {2} leva")]
		public decimal Price { get; init; }

		[Required]
		[StringLength(ImageUrlMaxLength)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; init; }

		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		[Display(Name = "Description")]
		public string? Description { get; set; }

		public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
	}
}
