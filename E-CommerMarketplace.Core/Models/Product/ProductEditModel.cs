using E_CommerceMarketplace.Core.Models.Vendor;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Product
{
	public class ProductEditModel : ProductModel
	{
        [Display(Name = "Status")]
        public int StatusId { get; set; }
		public IEnumerable<ProductStatusesModel> ProductStatuses { get; set; } = new List<ProductStatusesModel>();
	}
}
