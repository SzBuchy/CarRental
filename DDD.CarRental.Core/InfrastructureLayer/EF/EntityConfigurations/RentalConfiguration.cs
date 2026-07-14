using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> rentalConfiguration)
        {
            rentalConfiguration.HasKey(r => r.Id);
            rentalConfiguration.Ignore(r => r.DomainEvents);

            rentalConfiguration.Property(r => r.StartedAt).IsRequired();
            rentalConfiguration.Property(r => r.FinishedAt);
            rentalConfiguration.Property(r => r.TotalAmount).IsRequired();
            rentalConfiguration.Ignore(r => r.FreeMinutesPolicy);

            rentalConfiguration.OwnsOne(r => r.RentedCar, rentedCar =>
            {
                rentedCar.Property(c => c.CarId).HasColumnName("RentedCarId").IsRequired();
                rentedCar.Property(c => c.RegistrationNumber).HasColumnName("RentedCarRegistrationNumber").IsRequired().HasMaxLength(32);
                rentedCar.Property(c => c.DailyRate).HasColumnName("RentedCarDailyRate").IsRequired();
            });

            rentalConfiguration.OwnsOne(r => r.Renter, renter =>
            {
                renter.Property(r => r.DriverId).HasColumnName("RenterDriverId").IsRequired();
                renter.Property(r => r.LicenceNumber).HasColumnName("RenterLicenceNumber").IsRequired().HasMaxLength(32);
                renter.Property(r => r.FirstName).HasColumnName("RenterFirstName").IsRequired().HasMaxLength(64);
                renter.Property(r => r.LastName).HasColumnName("RenterLastName").IsRequired().HasMaxLength(64);
            });

            rentalConfiguration.OwnsOne(r => r.Total, money =>
            {
                money.Property(m => m.Amount).HasColumnName("TotalAmountValue").IsRequired();
                money.Property(m => m.Currency).HasColumnName("TotalCurrency").IsRequired().HasMaxLength(8);
            });
        }
    }
}
