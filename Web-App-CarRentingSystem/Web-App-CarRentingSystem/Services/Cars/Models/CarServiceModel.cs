﻿namespace Web_App_CarRentingSystem.Services.Cars.Models
{
    public class CarServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }

        public string CategoryName { get; set; }
    }
}
