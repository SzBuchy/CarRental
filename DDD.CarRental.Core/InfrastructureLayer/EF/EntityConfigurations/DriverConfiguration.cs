using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRental.Core.InfrastructureLayer.EF.EntityConfigurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> driverConfiguration)
        {
            // ustawianie klucza głównego
            driverConfiguration.HasKey(c => c.Id);

            // klucz tabeli nie będzie generowany przez EF
            driverConfiguration.Property(v => v.Id).ValueGeneratedNever();

            // wykluczenie DomainsEvents z modelu relacyjnego - nie ma potrzeby zapisywania w bazie zdarzeń domenowych
            driverConfiguration.Ignore(c => c.DomainEvents);

            driverConfiguration.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            driverConfiguration.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(64);

            driverConfiguration.Property(c => c.LicenceNumber)
                .IsRequired()
                .HasMaxLength(32);

            driverConfiguration.Property(c => c.FreeMinutes)
                .IsRequired();
        }
    }

    
}
