using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Data;

namespace Web_App_CarRentingSystem.Tests.Mocks
{
   public static class DatabaseMock
    {
        public static CarRentingDbContext Instance
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarRentingDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

                return new CarRentingDbContext(dbContextOptions);
            }
        }
    }
}
