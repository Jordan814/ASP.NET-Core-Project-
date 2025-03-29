using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Infrastructure;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Services.Cars;
using Web_App_CarRentingSystem.Services.Dealers;


namespace Web_App_CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {

        private readonly IDealerService dealers;
        private readonly ICarService cars;

        public CarsController(ICarService cars, IDealerService dealers)
        {
            this.dealers = dealers;
            this.cars = cars;
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


        [Authorize]
        public IActionResult Mine()
        {
            var myCars = this.cars.ByUser(this.User.GetId());

            return View(myCars);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new CarFromModel
            {
                Categories = this.cars.AllCarCategories()
            });
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFromModel car)
        {
            var dealerId = this.dealers.IdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (this.cars.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.cars.AllCarCategories();

                return View(car);
            }

                this.cars.Create(
                car.Brand,
                car.Model,
                car.Year,
                car.Image,
                car.Description,
                car.CategoryId,
                dealerId);


            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();
            var car = this.cars.Details(id);

            if(!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (car.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }
            var carForm = new CarFromModel
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                Year = car.Year,
                Image = car.ImageUrl,
                CategoryId = car.CategoryId,
                Categories = this.cars.AllCarCategories()
            };
            return View(carForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CarFromModel car)
        {
            var dealerId = this.dealers.IdByUser(this.User.GetId());
            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }
            if (car.CategoryId != 0 && !this.cars.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                car.Categories = this.cars.AllCarCategories();
                return View(car);
            }
            if (!this.cars.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var carIsEdited =  this.cars.Edit(
                id,
                car.Brand,
                car.Model,
                car.Year,
                car.Image,
                car.Description,
                car.CategoryId);

            if (!carIsEdited)
            {
                return BadRequest();
            }

            
            return RedirectToAction(nameof(All));
        }
    }
}
