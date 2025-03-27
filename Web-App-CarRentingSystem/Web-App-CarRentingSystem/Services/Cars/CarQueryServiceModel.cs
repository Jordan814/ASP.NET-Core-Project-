using Web_App_CarRentingSystem.Models.API.Cars;

namespace Web_App_CarRentingSystem.Services.Cars
{
    public class CarQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CarsPerPage { get; init; }

        public int TotalCars { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; init; }

    }
}
