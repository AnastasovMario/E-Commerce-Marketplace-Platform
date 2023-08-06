using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Statistics;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repo;
        public StatisticsService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task<StatisticsServiceModel> Total()
        {
            int totalProducts = await repo.AllReadonly<Product>()
                .CountAsync();
            int totalAvailableProducts = await repo.AllReadonly<Product>()
                .CountAsync(p => p.Status_Id == 4);

            return new()
            {
                TotalProducts = totalProducts,
                TotalAvailableProducts = totalAvailableProducts
            };
        }
    }
}
