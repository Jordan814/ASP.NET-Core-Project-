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

       

        public IActionResult All([FromQuery]AllCarsQueryViewModel query)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchTerm = query.SearchTerm.ToLower();
                carsQuery = carsQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(searchTerm) ||
                    c.Description.ToLower().Contains(searchTerm));
            }



            carsQuery = query.Sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(c => c.Id),
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsQueryViewModel.CarsPerPage)
                .Take(AllCarsQueryViewModel.CarsPerPage)
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.Image
                })
                .ToList();

            var carBrands = this.data.Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalCars = totalCars;
            query.Brands = carBrands;
            query.Cars = cars;

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
