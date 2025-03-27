using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Services.Cars;


namespace Web_App_CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {

        private readonly CarRentingDbContext data;
        private readonly ICarService cars;

        public CarsController(ICarService cars, CarRentingDbContext data)
        {
            this.cars = cars;
            this.data = data;
        }

        public IActionResult All([FromQuery]AllCarsQueryViewModel query)
        {
            var queryResult = this.cars.All(query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryViewModel.CarsPerPage);

            var carBrands = this.cars.AllBrands();

            query.TotalCars = queryResult.TotalCars;
            query.Brands = carBrands;
            query.Cars = queryResult.Cars;


            return View(query);
        }

        public IActionResult Add()
        {
            return View(new AddCarFromModel
            {
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddCarFromModel car)
        {
            if (!this.data.Categories.Any(c => c.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Image = car.Image,
                Year = car.Year,
                Description = car.Description,
                CategoryId = car.CategoryId
            };

            try
            {
                this.data.Cars.Add(carData);
                this.data.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving car data: {ex.Message}");
                // Optionally, add a model error to display a message to the user
                ModelState.AddModelError(string.Empty, "An error occurred while saving the car data.");
                car.Categories = this.GetCarCategories();
                return View(car);
            }

            return RedirectToAction(nameof(All));
        }



        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        {
            return this.data.Categories.Select(c => new CarCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}
