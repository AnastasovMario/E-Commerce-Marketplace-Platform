using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Product
{
    public class ProductHomeModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(100)]
        public string Name { get; init; } = null!;

        [Required]
        [Display(Name = "Price per month")]
        [Range(0.00, 2000.00, ErrorMessage = "Price per month must be a positive number and less than {2} leva")]
        public decimal Price { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
    }
}
