using System;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Factories
{
    public class RenterFactory
    {
        public Renter CreateFromDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            return new Renter(
                driver.Id,
                driver.LicenceNumber,
                driver.FirstName,
                driver.LastName
            );
        }
    }
}
