using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Models.Dealers;
using System.Linq;
using System.Security.Claims;

namespace Web_App_CarRentingSystem.Controllers
{
    public class DealersController : Controller
    {
        private readonly CarRentingDbContext data;

        public DealersController(CarRentingDbContext data)
        {
            this.data = data;
        }

        [Authorize]

        public IActionResult Become() { return View(); }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFromModel dealer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userIdAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            

            return RedirectToAction(nameof(CarsController.All), "Cars");
        }
    }
}
