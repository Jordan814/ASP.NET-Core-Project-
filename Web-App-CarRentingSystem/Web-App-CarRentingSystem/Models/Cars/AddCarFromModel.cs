using System.ComponentModel.DataAnnotations;
using Web_App_CarRentingSystem.Data.Models;
using static Web_App_CarRentingSystem.Data.DataConstants;

namespace Web_App_CarRentingSystem.Models.Cars
{
    public class AddCarFromModel
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
        [StringLength(CarDescriptionMaxLength, MinimumLength = CarBrandMinLength)]
        public string Description { get; init; }

        [Display(Name = "Category")]
        public int  CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}
