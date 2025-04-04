using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Models;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Models.Home;
using Web_App_CarRentingSystem.Services.Cars;
using Web_App_CarRentingSystem.Services.Cars.Models;
using Web_App_CarRentingSystem.Services.Statistics;

namespace Web_App_CarRentingSystem.Controllers;

public class HomeController : Controller
{
    private readonly IStatisticsService statistics;
    private readonly ICarService cars;
    private readonly IMapper mapper;
    private object value1;
    private object value2;

    public HomeController(IStatisticsService statistics,
        ICarService cars, IMapper mapper)
    {
        this.mapper = mapper;
        this.statistics = statistics;
        this.cars = cars;
    }

   

    public IActionResult Index()
    {
       

        var latestCars = this.cars.Latest().ToList();

        var totalStatistics = this.statistics.Total();  

        return View(new IndexViewModel
        {
            TotalCars = totalStatistics.TotalCars,
            TotalUsers = totalStatistics.TotalUsers,
            Cars = latestCars
        });
    }

    [ResponseCache]
    public IActionResult Error()
    {
        return View();
    }
}
