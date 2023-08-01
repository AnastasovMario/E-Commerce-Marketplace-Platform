using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Total { get; init; }

        [Required]
        public DateTime Date { get; set; }

		[ForeignKey(nameof(Buyer))]
		public string Buyer_Id { get; set; }

		public ApplicationUser Buyer { get; set; }

		public ICollection<ProductSale> ProductSales { get; set; }
	}
}
