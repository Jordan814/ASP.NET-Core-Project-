using System.ComponentModel.DataAnnotations;

namespace Web_App_CarRentingSystem.Data.Models
{
    public class Dealer 
    {

        public int Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();


    }
}
