using E_CommerceMarketplace.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Product
{
    public class ProductServiceModel : IProductModel
    {
        public int Id { get; set; }

		public string Name { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public string Status { get; set; }

        public bool IsBought { get; set; }
    }
}
