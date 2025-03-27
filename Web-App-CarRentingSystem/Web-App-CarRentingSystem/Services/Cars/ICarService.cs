using Web_App_CarRentingSystem.Models.Cars;

namespace Web_App_CarRentingSystem.Services.Cars
{
    public interface ICarService
    {

        CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage);

        IEnumerable<string> AllBrands();
    }
}
