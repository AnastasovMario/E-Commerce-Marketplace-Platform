using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;

namespace E_Commerce_Marketplace_Platform.Extensions.DependencyInjection
{
    public static class ECommerServiceCollectionExtension 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IItemService, ItemService>();
			return services;
        }
    }
}
