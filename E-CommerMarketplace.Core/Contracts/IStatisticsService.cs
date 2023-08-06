using E_CommerceMarketplace.Core.Models.Statistics;

namespace E_CommerceMarketplace.Core.Contracts
{
    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
