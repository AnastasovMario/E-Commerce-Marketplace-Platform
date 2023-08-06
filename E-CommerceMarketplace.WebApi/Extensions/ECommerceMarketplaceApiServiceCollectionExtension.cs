using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using E_CommerceMarketplace.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ECommerceMarketplaceApiServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }

        public static IServiceCollection AddECommerceMarketplaceDbContext(this IServiceCollection services, IConfiguration config)
        {

            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
