using E_CommerceMarketplace.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceMarketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductHomeModel>> GetLastProducts();

        Task<IEnumerable<ProductCategoryModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);
    }
}
