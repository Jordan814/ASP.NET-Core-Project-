using System.ComponentModel.DataAnnotations;
using Web_App_CarRentingSystem.Models.Cars;

namespace Web_App_CarRentingSystem.Models.API.Cars
{
    public class AllCarsApiRequestModel
    {

        public int CarsPerPage { get; init; } = 10;

        public string Brand { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

    }
}
