using System.ComponentModel.DataAnnotations;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Services.Cars;
using static Web_App_CarRentingSystem.Data.DataConstants;

namespace Web_App_CarRentingSystem.Models.Cars
{
    public class CarFromModel
    {
        [Required]
        [StringLength(CarBrandMaxLength, MinimumLength = CarBrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength = CarModelMinLength)]
        public string Model { get; init; }

        [Required]
        [Url]
        public string Image { get; init; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; init; }

        [Required]
     
        public string Description { get; init; }

        [Display(Name = "Category")]
        public int  CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
    }
}
