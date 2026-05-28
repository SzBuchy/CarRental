using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarRentalDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DriverConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new RentalConfiguration());
        }
    }
}
