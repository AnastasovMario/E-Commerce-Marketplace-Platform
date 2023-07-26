using E_CommerceMarketplace.Infrastructure.Common;

namespace E_Commerce_Marketplace_Platform.Extensions.DependencyInjection
{
    public static class ECommerServiceCollectionExtension 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            return services;
        }
    }
}
