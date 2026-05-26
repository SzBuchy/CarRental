using System;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.DomainModelLayer.Policies;

namespace DDD.CarRental.Core.DomainModelLayer.Factories
{
    public class RentalFactory
    {
        private readonly RenterFactory _renterFactory;
        private readonly RentedCarFactory _rentedCarFactory;

        public RentalFactory()
            : this(new RenterFactory(), new RentedCarFactory())
        {
        }

        public RentalFactory(RenterFactory renterFactory, RentedCarFactory rentedCarFactory)
        {
            _renterFactory = renterFactory ?? throw new ArgumentNullException(nameof(renterFactory));
            _rentedCarFactory = rentedCarFactory ?? throw new ArgumentNullException(nameof(rentedCarFactory));
        }

        public Rental Create(Driver driver, Car car, DateTime startedAt)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            car.Rent();

            var rental = new Rental(
                _renterFactory.CreateFromDriver(driver),
                _rentedCarFactory.CreateFromCar(car),
                startedAt,
                new FreeMinutesPolicy()
            );

            rental.AddDomainEvent(new RentalStartedEvent(rental.Id, driver.Id, car.Id, startedAt));

            return rental;
        }
    }
}
