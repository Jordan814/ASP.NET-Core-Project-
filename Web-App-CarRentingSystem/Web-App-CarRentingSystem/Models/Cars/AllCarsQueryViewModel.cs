using System.ComponentModel.DataAnnotations;
using Web_App_CarRentingSystem.Services.Cars.Models;

namespace Web_App_CarRentingSystem.Models.Cars
{
    public class AllCarsQueryViewModel
    {
        public const int CarsPerPage = 5;


        public string Brand { get; init; }

        public IEnumerable<CarServiceModel> Cars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCars { get; set; }

    }
}
