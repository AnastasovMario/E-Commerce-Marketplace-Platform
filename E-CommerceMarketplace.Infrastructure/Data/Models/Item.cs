using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }

		[Required]
        [Range(MinimumQuantity, MaximumQuantity)]
        public int Quantity { get; set; }

		[Required]
		[Column(TypeName = "money")]
		[Precision(18, 2)]
		public decimal Total { get; set; }

		[ForeignKey(nameof(Product))]
		public int Product_Id { get; set; }

		public Product Product { get; set; }

		[ForeignKey(nameof(Order))]
		public int Order_Id { get; set; }

		public Order Order { get; set; }
	}
}
