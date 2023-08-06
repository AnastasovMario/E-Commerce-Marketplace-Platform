using E_CommerceMarketplace.Core.Models.Product;

namespace E_CommerceMarketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductHomeModel>> GetLastProducts();

        Task<IEnumerable<ProductCategoryModel>> AllCategories();

		Task<IEnumerable<ProductStatusesModel>> AllProductStatuses();

		Task<bool> CategoryExists(int categoryId);

        Task<bool> StatusExists(int statusId);

        Task<int> Create(ProductModel productModel, int vendorId);

		Task<int> Edit(int productId, ProductEditModel productModel);

        Task<bool> Exists(int productId);

        Task<ProductDetailsServiceModel> GetProductDetailsById(int productId);

        Task<bool> HasVendorWithId(int productId, string userId);

        Task<int> GetProductCategoryId(int productId);

        Task<int> GetProductStatusId(int productId);

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<IEnumerable<string>> AllStatusesNames();

        Task<List<ProductServiceModel>> GetProductsByVendorId(int vendorId);

        Task Delete(int productId);

        Task<ProductQueryServiceModel> All(string? category = null,
            string? status = null,
            string? searchTerm = null,
            ProductSorting sorting = ProductSorting.Newest,
            int currentPage = 1,
            int productPage = 1);

        Task<OrderServiceModel> Buy(int productId);
    }
}
