using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(DbConstants.CategoryNameLength)]
        public string Name { get; set; } = null!;

        public List<Product> Products { get; init; }
    }
}
