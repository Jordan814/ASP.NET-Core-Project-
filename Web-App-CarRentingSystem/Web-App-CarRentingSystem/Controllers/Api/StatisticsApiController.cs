using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Services.Statistics;

namespace Web_App_CarRentingSystem.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase   
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {

           return this.statistics.Total();
        }
    }
}
