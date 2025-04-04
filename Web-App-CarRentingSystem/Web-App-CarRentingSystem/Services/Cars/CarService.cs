using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Web_App_CarRentingSystem.Models.API.Cars;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Data;
using System.Linq;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Services.Cars.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Web_App_CarRentingSystem.Services.Cars
{
    public class CarService : ICarService
    {
       private readonly CarRentingDbContext data;
        private readonly IMapper mapper;

        public CarService(CarRentingDbContext data, IMapper mapper)
        {
            this.mapper = mapper;
            this.data = data;
        }
        public CarQueryServiceModel All(string brand, string searchTerm, CarSorting sorting, int currentPage, int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(searchTerm) ||
                    c.Description.ToLower().Contains(searchTerm));
            }



            carsQuery = sorting switch
            {
                CarSorting.DateCreated => carsQuery.OrderByDescending(c => c.Id),
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                _ => carsQuery.OrderByDescending(c => c.Id)
            };
            var totalCars = carsQuery.Count();

            var cars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage));


            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };

        }
        public IEnumerable<CarServiceModel> ByUser(string userId)
        {
           return this.GetCars(this.data.Cars.Where(c => c.Dealer.UserId == userId));
        }

        public IEnumerable<string> AllBrands()
        {
            return this.data.Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
        }

     
        private IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
        {
            return carQuery
                .Select(c => new CarServiceModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.Image,
                    CategoryName = c.Category.Name
                })
                .ToList();
        }

        public IEnumerable<CarCategoryServiceModel> AllCarCategories()
        {
            return this.data.Categories.Select(c => new CarCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public CarDetailsServiceModel Details(int id)
        {
            return this.data.Cars.Where(c => c.Id == id)
                .ProjectTo<CarDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public int Create(string brand, string model, int year, string imageUrl, string description, int categoryId, int dealerId)
        {
            var carData = new Car
            {
                Brand = brand,
                Model = model,
                Year = year,
                Image = imageUrl,
                Description = description,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            // Log before adding the car to the database
            Console.WriteLine("Adding car to database: Brand={0}, Model={1}, Year={2}, Image={3}, Description={4}, CategoryId={5}, DealerId={6}",
                brand, model, year, imageUrl, description, categoryId, dealerId);

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            // Log after successful save
            Console.WriteLine("Car added to database successfully");

            return carData.Id;
        }


        public bool Edit(int id, string brand, string model, int year, string imageUrl, string description, int categoryId)
        {
            var carData = this.data.Cars.Find(id);

            if(carData == null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Year = year;
            carData.Image = imageUrl;
            carData.Description = description;
            carData.CategoryId = categoryId;



            this.data.SaveChanges();

            return true;
        }

        public bool IsByDealer(int carId, int dealerId)
        {
            return this.data.Cars.Any(c => c.Id == carId && c.DealerId == dealerId);
        }

        public IEnumerable<LatestCarServiceModel> Latest()
        {
            return this.data.Cars.OrderByDescending(c => c.Id)
                 .ProjectTo<LatestCarServiceModel>(this.mapper.ConfigurationProvider)
                 .Take(3)
                 .ToList();
        }
    }
}
