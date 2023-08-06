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
        [Display(Name = "Price")]
        public decimal Price { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; init; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; } = null!;

        public string Vendor { get; set; } = null!;

        public string Status { get; set; } = null!;

        public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
    }
}
