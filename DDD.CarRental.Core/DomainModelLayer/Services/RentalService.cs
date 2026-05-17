using System;
using DDD.CarRental.Core.DomainModelLayer.Factories;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.DomainModelLayer.Policies;
using DDD.SharedKernel.DomainModelLayer;

namespace DDD.CarRental.Core.DomainModelLayer.Services
{
    public class RentalService : IDomainService
    {
        private readonly RentalFactory _rentalFactory;
        private readonly RentalPricingPolicy _rentalPricingPolicy;

        public RentalService()
            : this(new RentalFactory(), new RentalPricingPolicy())
        {
        }

        public RentalService(RentalFactory rentalFactory, RentalPricingPolicy rentalPricingPolicy)
        {
            _rentalFactory = rentalFactory ?? throw new ArgumentNullException(nameof(rentalFactory));
            _rentalPricingPolicy = rentalPricingPolicy ?? throw new ArgumentNullException(nameof(rentalPricingPolicy));
        }

        public Rental RentCar(Driver driver, Car car, DateTime startedAt)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            return _rentalFactory.Create(driver, car, startedAt);
        }

        public void FinishRental(Rental rental, Car car, DateTime finishedAt)
        {
            if (rental == null)
            {
                throw new ArgumentNullException(nameof(rental));
            }

            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            rental.Finish(finishedAt, _rentalPricingPolicy);
            car.Return();
        }
    }
}
