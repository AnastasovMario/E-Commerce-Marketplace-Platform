using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ECommerServiceCollectionExtension 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IItemService, ItemService>();
			services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
