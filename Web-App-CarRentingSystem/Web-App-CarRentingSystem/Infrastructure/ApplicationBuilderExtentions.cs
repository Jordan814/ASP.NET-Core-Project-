using Microsoft.EntityFrameworkCore;
using Web_App_CarRentingSystem.Data;
using Web_App_CarRentingSystem.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_App_CarRentingSystem.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var data = services.GetRequiredService<CarRentingDbContext>();
                data.Database.Migrate();

               SeedCategoies(data);
            }
            
            
            return app;
        }

        private static void SeedCategoies(CarRentingDbContext data)
        {
            if (!data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Economy"},
                new Category {Name = "Midsize"},
                new Category {Name = "Luxury"},
                new Category {Name = "SUV"},
                new Category {Name = "Van"},
                new Category {Name = "Truck"},
            });

            data.SaveChanges();
        }
        
    }
}
