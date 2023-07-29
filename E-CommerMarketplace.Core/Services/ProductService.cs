using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
	public class ProductService : IProductService
	{
		private readonly IRepository repo;
		public ProductService(IRepository _repo)
		{
			repo = _repo;
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

			await repo.AddAsync(product);
			await repo.SaveChangesAsync();


			return product.Id;
		}

		public async Task<int> Edit(int productId, ProductEditModel productModel)
		{
			var product = await repo.GetByIdAsync<Product>(productId);

			product.Name = productModel.Name;
			product.Price = productModel.Price;
			product.ImageUrl = productModel.ImageUrl;
			product.Description = productModel.Description;
			product.Category_Id = productModel.CategoryId;
			product.Status_Id = productModel.StatusId;

			await repo.SaveChangesAsync();

			return product.Id;
		}

        public async Task<bool> Exists(int productId)
        {
			return await repo.AllReadonly<Product>()
				.AnyAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<ProductHomeModel>> GetLastProducts()
		{
			return await repo.AllReadonly<Product>()
				.OrderByDescending(p => p.Id)
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

        public async Task<int> GetProductStatusId(int productId)
        {
            return await repo.AllReadonly<Product>()
                .Where(p => p.Id == productId)
                .Select(p => p.Status_Id)
                .FirstOrDefaultAsync();
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
