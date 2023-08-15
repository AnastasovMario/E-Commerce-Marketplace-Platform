using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Price { get; set; }

		[Required]
		[StringLength(ImageUrlMaxLength)]
		public string ImageUrl { get; set; } = null!;

		[ForeignKey(nameof(Category))]
        public int Category_Id { get; set; }

        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Vendor))]
        public int Vendor_Id { get; set; } 

        public Vendor Vendor { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

		[ForeignKey(nameof(Status))]
		public int Status_Id { get; set; }

        public Status Status { get; set; } = null!;
	}
}
