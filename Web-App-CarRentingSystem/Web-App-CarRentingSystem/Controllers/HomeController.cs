using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Models;

namespace Web_App_CarRentingSystem.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
