using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Web_App_CarRentingSystem.Data.Models
{
    public class User : IdentityUser
    {

        [MaxLength(50)]
        public string FullName { get; set; }

    }
}
