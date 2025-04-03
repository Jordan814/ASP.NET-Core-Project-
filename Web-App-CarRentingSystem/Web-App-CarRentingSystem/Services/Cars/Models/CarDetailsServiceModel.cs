namespace Web_App_CarRentingSystem.Services.Cars.Models
{
    public class CarDetailsServiceModel : CarServiceModel
    {

        public string Description { get; init; }

        public int DealerId { get; set; }

        public string DealerName { get; set; }

        public int CategoryId { get; init; }

        public string UserId { get; set; }

    }
}
