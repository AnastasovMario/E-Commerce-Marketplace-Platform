using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceMarketplace.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService service;

        public StatisticsApiController(IStatisticsService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Gets statistics about number of houses and rented houses
        /// </summary>
        /// <returns>Total houses and total rents</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await service.Total();

            return Ok(model);
        }
    }
}
