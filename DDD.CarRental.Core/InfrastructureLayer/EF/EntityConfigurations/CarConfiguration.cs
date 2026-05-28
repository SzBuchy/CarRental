using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> carConfiguration)
        {
            carConfiguration.HasKey(c => c.Id);
            carConfiguration.Ignore(c => c.DomainEvents);

            carConfiguration.Property(c => c.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(32);

            carConfiguration.Property(c => c.DailyRate)
                .IsRequired();

            carConfiguration.Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();

            carConfiguration.OwnsOne(c => c.CurrentPosition, position =>
            {
                position.Property(p => p.X).HasColumnName("CurrentPositionX").IsRequired();
                position.Property(p => p.Y).HasColumnName("CurrentPositionY").IsRequired();
                position.Property(p => p.Unit).HasColumnName("CurrentPositionUnit").IsRequired().HasMaxLength(16);
            });

            carConfiguration.OwnsOne(c => c.CurrentDistance, distance =>
            {
                distance.Property(d => d.Value).HasColumnName("CurrentDistanceValue").IsRequired();
                distance.Property(d => d.Unit).HasColumnName("CurrentDistanceUnit").IsRequired().HasMaxLength(16);
            });

            carConfiguration.OwnsOne(c => c.TotalDistance, distance =>
            {
                distance.Property(d => d.Value).HasColumnName("TotalDistanceValue").IsRequired();
                distance.Property(d => d.Unit).HasColumnName("TotalDistanceUnit").IsRequired().HasMaxLength(16);
            });
        }
    }
}
