using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Models;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Models.Home;
using Web_App_CarRentingSystem.Services.Statistics;

namespace Web_App_CarRentingSystem.Controllers;

public class HomeController : Controller
{
    private readonly IStatisticsService statistics;
    private readonly CarRentingDbContext data;

    public HomeController(IStatisticsService statistics,
        CarRentingDbContext data)
    {
        this.statistics = statistics;
        this.data = data;
    }

    public IActionResult Index()
    {
       

        var cars = this.data.Cars.OrderByDescending(c => c.Id)
                 .Select(c => new CarIndexViewModel
                 {
                     Id = c.Id,
                     Brand = c.Brand,
                     Model = c.Model,
                     Year = c.Year,
                     ImageUrl = c.Image,
                 }).Take(3)
                 .ToList();

        var totalStatistics = this.statistics.Total();  

        return View(new IndexViewModel
        {
            TotalCars = totalStatistics.TotalCars,
            TotalUsers = totalStatistics.TotalUsers,
            Cars = cars
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
