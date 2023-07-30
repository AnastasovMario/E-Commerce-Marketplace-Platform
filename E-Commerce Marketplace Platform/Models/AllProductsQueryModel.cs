using E_CommerceMarketplace.Core.Models.Product;

namespace E_Commerce_Marketplace_Platform.Models
{
    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 3;

        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? SearchTerm { get; set; }

        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalProductsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> Statuses { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<ProductServiceModel> Products { get; set; } = Enumerable.Empty<ProductServiceModel>();
    }
}
