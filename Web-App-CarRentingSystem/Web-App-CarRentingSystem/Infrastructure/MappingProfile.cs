using AutoMapper;
using Web_App_CarRentingSystem.Data.Models;
using Web_App_CarRentingSystem.Models.Cars;
using Web_App_CarRentingSystem.Services.Cars.Models;

namespace Web_App_CarRentingSystem.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            this.CreateMap<CarDetailsServiceModel, CarFromModel>();
            this.CreateMap<Car, LatestCarServiceModel>();
            this.CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
