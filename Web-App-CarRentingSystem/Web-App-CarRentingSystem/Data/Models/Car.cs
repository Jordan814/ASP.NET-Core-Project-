﻿using System.ComponentModel.DataAnnotations;

namespace Web_App_CarRentingSystem.Data.Models;
using static DataConstants;

public class Car
{
        public int Id { get; init; }

        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxLength)]
         public string Model { get; set; }

        [Required]
        [MaxLength(CarDescriptionMaxLength)]
         public string Description { get; set; }
       
        [Required]
        public string Image { get; set; }
        
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }

}

