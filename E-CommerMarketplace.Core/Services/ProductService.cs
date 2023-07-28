using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
            return await repo.AllReadonly<Product>()
                .Select(p => new ProductCategoryModel()
                {
                    Id= p.Id,
                    Name= p.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await repo.AllReadonly<Category>()
                .AnyAsync(c => c.Id == categoryId);
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
                    Description = p.Description,
                    Vendor = p.Vendor.User.FirstName + " " + p.Vendor.User.LastName,
                })
                .ToListAsync();
        }

    }
}
