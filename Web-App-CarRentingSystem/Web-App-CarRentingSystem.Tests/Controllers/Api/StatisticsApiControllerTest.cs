using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Controllers.Api;
using Web_App_CarRentingSystem.Tests.Mocks;
using Xunit;

namespace Web_App_CarRentingSystem.Tests.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatistics_ShouldReturnStatistics()
        {
            
            var statisticsApiController = new StatisticsApiController(StatisticsServiceMock.Instance);
            
            var result = statisticsApiController.GetStatistics();
            
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCars);
            Assert.Equal(10, result.TotalRents);
            Assert.Equal(15, result.TotalUsers);

        }
    }
}
