using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Product> Products { get; init; }

        [NotMapped]
        public static Category Electronics { get; set; } = new() { Id = 1 };

        [NotMapped]
        public static Category ClothingAndFashion { get; set; } = new() { Id = 2 };

        [NotMapped]
        public static Category HomeAndGarden { get; set; } = new() { Id = 3 };

        [NotMapped]
        public static Category HealthAndBeauty { get; set; } = new() { Id = 4 };

        [NotMapped]
        public static Category BooksAndMagazines { get; set; } = new() { Id = 5 };
    }
}
