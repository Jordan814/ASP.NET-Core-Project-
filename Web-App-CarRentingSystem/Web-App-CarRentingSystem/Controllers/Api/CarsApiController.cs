using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Web_App_CarRentingSystem.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;
using System;
using Web_App_CarRentingSystem.Models.API.Cars;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Services.Cars;
using Web_App_CarRentingSystem.Services.Cars.Models;


namespace Web_App_CarRentingSystem.Controllers.Api
{
   
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService cars;

        public CarsApiController(ICarService cars)
        {
            this.cars = cars;
        }

        [HttpGet]
        public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query) { 


            return this.cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage
            );
        }

    }
}
