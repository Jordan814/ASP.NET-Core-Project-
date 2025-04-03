using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Services.Cars.Models;

namespace Web_App_CarRentingSystem.Services.Cars
{
    public interface ICarService
    {

        int Create(string brand, string model, int year, string imageUrl, string description, int categoryId, int dealerId);

        bool Edit(int carId, string brand, string model, int year, string imageUrl, string description, int categoryId);

        bool IsByDealer(int carId, int dealerId);

        CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage);

        CarDetailsServiceModel Details(int carId);

        IEnumerable<CarServiceModel> ByUser(string userId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCarCategories();

        bool CategoryExists(int categoryId);
    }
}
