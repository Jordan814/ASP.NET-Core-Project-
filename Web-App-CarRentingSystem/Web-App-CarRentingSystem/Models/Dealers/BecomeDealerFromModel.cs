using System.ComponentModel.DataAnnotations;

namespace Web_App_CarRentingSystem.Models.Dealers
{
    public class BecomeDealerFromModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}