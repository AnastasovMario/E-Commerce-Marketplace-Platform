using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Name { get; init; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Price { get; init; }

        [Required]
        public string ImageUrl { get; init; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; init; }

        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Vendor))]
        public int VendorId { get; init; } 

        public Vendor Vendor { get; init; } = null!;
    }
}
