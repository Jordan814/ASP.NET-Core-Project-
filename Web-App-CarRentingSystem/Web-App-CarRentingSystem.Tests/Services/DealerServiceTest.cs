using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Services.Dealers;
using Web_App_CarRentingSystem.Tests.Mocks;
using Xunit;

namespace Web_App_CarRentingSystem.Tests.Services
{
    public class DealerServiceTest
    {
        [Fact]
        public void IsDealerShouldRetuntTrueWhenUserIsDealer()
        {
            using var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer { UserId = "TestUserId" });
            data.SaveChanges();

            var dealerService = new DealerService(data);


            var result = dealerService.IsDealer("TestUserId");

            Assert.True(result); 
        }


    }
}
