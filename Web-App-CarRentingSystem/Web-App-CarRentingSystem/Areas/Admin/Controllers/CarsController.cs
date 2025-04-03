using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Web_App_CarRentingSystem.Areas.Admin.AdminConstants;

namespace Web_App_CarRentingSystem.Areas.Admin.Controllers
{
   
    public class CarsController : AdminController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
