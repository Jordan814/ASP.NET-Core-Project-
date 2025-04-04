using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App_CarRentingSystem.Infrastructure;

namespace Web_App_CarRentingSystem.Tests.Mocks
{
    public class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });
                
                return new Mapper(mapperConfiguration);
            }
        }
    }
}
