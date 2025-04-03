using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Web_App_CarRentingSystem.Areas.Admin.AdminConstants;

namespace Web_App_CarRentingSystem.Areas.Admin.Controllers
{
    [Area(areaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
        public AdminController()
        {
            
        }
    }
}
