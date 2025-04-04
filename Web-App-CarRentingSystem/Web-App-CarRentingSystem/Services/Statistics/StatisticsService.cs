﻿using Web_App_CarRentingSystem.Data;
using System.Linq;
namespace Web_App_CarRentingSystem.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly CarRentingDbContext data;

        public StatisticsService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
           var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers
            };

        }
    }
}
