using Web_App_CarRentingSystem.Data;

namespace Web_App_CarRentingSystem.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly CarRentingDbContext data;

        public DealerService(CarRentingDbContext data)
        {
            this.data = data;
        }

        public int IdByUser(string userId)
        {
            return this.data.Dealers.Where(d => d.UserId == userId).Select(d => d.Id).FirstOrDefault();
        }

        public bool IsDealer(string userId)
        {
           return this.data.Dealers.Where(d => d.UserId == userId).Select(d => d.Id).Any();
        }

       
    }
}
