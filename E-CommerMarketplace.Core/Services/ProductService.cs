using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Core.Models.Vendor;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_CommerceMarketplace.Core.Services
{
	public class ProductService : IProductService
	{
		private readonly IRepository repo;

        private readonly ILogger logger;

        public ProductService(IRepository _repo,
            ILogger<ProductService> _logger)
		{
			repo = _repo;
            logger = _logger;
        }

        public async Task<ProductQueryServiceModel> All(string? category = null,
			string? status = null,
            string? searchTerm = null,
            ProductSorting sorting = ProductSorting.Newest,
			int currentPage = 1,
			int productsPerPage = 1)
        {
            var result = new ProductQueryServiceModel();
			var query = repo.AllReadonly<Product>();

			if (!string.IsNullOrEmpty(status))
			{
				query = query.Where(p => p.Status.Description == status);
			}

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.Name == category);
            }

			if (!string.IsNullOrEmpty(searchTerm))
            {
				query = query
					.Where(p => p.Name.Contains(searchTerm)
					|| p.Vendor.FirstName.Contains(searchTerm)
					|| p.Vendor.LastName.Contains(searchTerm));
			}

            query = sorting switch
            {
                ProductSorting.Price => query
                    .OrderBy(h => h.Price),
                ProductSorting.AvaiableFirst => query
                    .OrderBy(h => h.Status.Id == 4)
                    .ThenByDescending(h => h.Id),
                _ => query.OrderByDescending(h => h.Id)
            };

            var products = await query
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(h => new ProductServiceModel()
                {
                    Id = h.Id,
                    Name = h.Name,
                    ImageUrl = h.ImageUrl,
                    Price = h.Price,
					Status = h.Status.Description,
					Vendor = h.Vendor.FirstName + " " + h.Vendor.LastName,
					IsAvailable = h.Status.Id != 4
                })
                .ToListAsync();

            var totalHousesCount = await query.CountAsync();

            return new()
            {
                TotalProductsCount = totalHousesCount,
                Products = products,
            };
        }

        public async Task<IEnumerable<ProductCategoryModel>> AllCategories()
		{
			return await repo.AllReadonly<Category>()
				.Select(p => new ProductCategoryModel()
				{
					Id = p.Id,
					Name = p.Name,
				})
				.ToListAsync();
		}

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await repo.AllReadonly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductStatusesModel>> AllProductStatuses()
		{
			return await repo.AllReadonly<Status>()
				.OrderByDescending(s => s.Id)
				.Take(2)
				.Select(s => new ProductStatusesModel
				{
					Id = s.Id,
					Description = s.Description
				})
				.ToListAsync();
		}

        public async Task<IEnumerable<string>> AllStatusesNames()
        {
            return await repo.AllReadonly<Status>()
                .Select(c => c.Description)
                .Distinct()
                .ToListAsync();
        }

		public async Task<bool> CategoryExists(int categoryId)
		{
			return await repo.AllReadonly<Category>()
				.AnyAsync(c => c.Id == categoryId);
		}

		public async Task<int> Create(ProductModel model, int vendorId)
		{
			var product = new Product()
			{
				Name = model.Name,
				Price = model.Price,
				ImageUrl = model.ImageUrl,
				Category_Id = model.CategoryId,
				Vendor_Id = vendorId,
				Description = model.Description,
				Status_Id = 4,			
			};

			try
			{
                await repo.AddAsync(product);
                await repo.SaveChangesAsync();

            }
            catch (Exception ex)
			{
                logger.LogError(nameof(Create), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
			

			return product.Id;
		}

        public async Task Delete(int productId)
        {
            await repo.DeleteAsync<Product>(productId);
            await repo.SaveChangesAsync();
        }

        public async Task Edit(int productId, ProductEditModel productModel)
		{
			var product = await repo.GetByIdAsync<Product>(productId);

			product.Name = productModel.Name;
			product.Price = productModel.Price;
			product.ImageUrl = productModel.ImageUrl;
			product.Description = productModel.Description;
			product.Category_Id = productModel.CategoryId;
			product.Status_Id = productModel.StatusId;

			await repo.SaveChangesAsync();
		}

        public async Task<bool> Exists(int productId)
        {
			return await repo.AllReadonly<Product>()
				.AnyAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<ProductHomeModel>> GetLastProducts()
		{
			return await repo.AllReadonly<Product>()
				.OrderByDescending(p => p.Status_Id)
				.ThenByDescending(p => p.Id)
				.Take(3)
				.Select(p => new ProductHomeModel
				{
					Id = p.Id,
					Name = p.Name,
					Price = Math.Round(p.Price, 2, MidpointRounding.AwayFromZero),
					ImageUrl = p.ImageUrl,
					Category = p.Category.Name,
					Status = p.Status.Description,
					Vendor = p.Vendor.FirstName + " " + p.Vendor.LastName,
				})
				.ToListAsync();
		}

        public async Task<int> GetProductCategoryId(int productId)
        {
			return await repo.AllReadonly<Product>()
				.Where(p => p.Id == productId)
				.Select(p => p.Category_Id)
				.FirstOrDefaultAsync();
        }

		public async Task<ProductDetailsServiceModel> GetProductDetailsById(int productId)
		{
			return await repo.AllReadonly<Product>()
				.Where(p => p.Id == productId)
				.Select(p => new ProductDetailsServiceModel
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					Description = p.Description,
					Status = p.Status.Description,
					Category = p.Category.Name,
					ImageUrl = p.ImageUrl,
					IsAvailable = p.Status.Id != 4,
					Vendor = new VendorServiceModel
					{
						FirstName = p.Vendor.FirstName,
						LastName = p.Vendor.LastName,
						PhoneNumber = p.Vendor.PhoneNumber,
					}
				})
				.FirstAsync();
		}

        public async Task<List<ProductServiceModel>> GetProductsByVendorId(int vendorId)
        {
			return await repo.AllReadonly<Product>()
				.Where(p => p.Vendor_Id == vendorId)
				.Select(p => new ProductServiceModel
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					Status = p.Status.Description,
					ImageUrl = p.ImageUrl,
					IsAvailable = p.Status.Id != 4,
					Vendor = p.Vendor.FirstName + " " + p.Vendor.LastName
				})
				.ToListAsync();
        }

        public async Task<int> GetProductStatusId(int productId)
        {
            return await repo.AllReadonly<Product>()
                .Where(p => p.Id == productId)
                .Select(p => p.Status_Id)
                .FirstOrDefaultAsync();
        }

		public async Task<IEnumerable<ProductServiceModel>> GetUserProducts(string userId)
		{
			return await repo.AllReadonly<Product>()
				.Where(p => p.Vendor.User_Id == userId && p.Status_Id == 4)
				.Select(p => new ProductServiceModel
				{
					Name = p.Name,
					Price = p.Price,
					ImageUrl = p.ImageUrl,
					Status = p.Status.Description,
					Vendor = p.Vendor.FirstName + " " + p.Vendor.LastName
				})
				.ToListAsync();
		}

		public async Task<bool> HasVendorWithId(int productId, string userId)
        {
            bool result = false;
            var product = await repo.AllReadonly<Product>()
                .Where(p => p.Id == productId)
                .Include(h => h.Vendor)
                .FirstOrDefaultAsync();

            if (product?.Vendor != null && product.Vendor.User_Id == userId)
            {
                result = true;
            }

            return result;
        }

        public async Task<bool> StatusExists(int statusId)
        {
            return await repo.AllReadonly<Status>()
                .AnyAsync(s => s.Id == statusId);
        }
    }
}
