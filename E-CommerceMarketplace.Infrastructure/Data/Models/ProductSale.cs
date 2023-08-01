using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
	public class ProductSale
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }

		public int SaleId { get; set; }
		public Sale Sale { get; set; }
	}
}
