using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace FoodDelivery.Persistencia.Repositorios
{
    public class FoodDeliveryDbContextFactory : IDesignTimeDbContextFactory<FoodDeliveryDbContext>
    {
        public FoodDeliveryDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../FoodDeliveryApi.API");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FoodDeliveryDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new FoodDeliveryDbContext(optionsBuilder.Options);
        }
    }
}