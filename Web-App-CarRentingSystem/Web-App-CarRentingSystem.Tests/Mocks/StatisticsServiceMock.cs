using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Services.Statistics;

namespace Web_App_CarRentingSystem.Tests.Mocks
{
    public class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock.Setup( s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalCars = 10,
                        TotalRents = 5,
                        TotalUsers = 15,
                        
                    });

                return statisticsServiceMock.Object;
            }


        }
    }
}
