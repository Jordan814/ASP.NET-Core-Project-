﻿namespace Web_App_CarRentingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();


    }
}
