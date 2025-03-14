using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Models.Cars;

namespace Web_App_CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {

        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddCarFromModel
            {
                Categories  = this.GetCarCategories()
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

            this.data.Cars.Add(carData);

            this.data.SaveChanges();

            return RedirectToAction("Index","Home");

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
