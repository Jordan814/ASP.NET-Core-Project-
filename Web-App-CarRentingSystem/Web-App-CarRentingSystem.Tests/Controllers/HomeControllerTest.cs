using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Web_App_CarRentingSystem.Tests.Mocks;
using Web_App_CarRentingSystem.Services.Cars;
using Web_App_CarRentingSystem.Services.Statistics;
using Web_App_CarRentingSystem.Models.Home;
using Web_App_CarRentingSystem.Data.Models;

namespace Web_App_CarRentingSystem.Tests.Controller { 

public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var cars = Enumerable.Range(0, 10).Select(i => new Car());
            data.Cars.AddRange(cars);
            data.Users.Add(new User());

            data.SaveChanges();

            var carService = new CarService(data, mapper);

            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, carService, mapper);

            var result = homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(2, indexViewModel.Cars.Count);
            Assert.Equal(10, indexViewModel.TotalCars);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }
    }
}
