using System;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Factories
{
    public class RentedCarFactory
    {
        public RentedCar CreateFromCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            return new RentedCar(
                car.Id,
                car.RegistrationNumber,
                car.DailyRate
            );
        }
    }
}
