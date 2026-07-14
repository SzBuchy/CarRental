using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class RentedCar : ValueObject
    {
        public long CarId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public decimal DailyRate { get; private set; }

        private RentedCar()
        {
        }

        public RentedCar(long carId, string registrationNumber, decimal dailyRate)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)){throw new ArgumentException($"{nameof(registrationNumber)} cannot be null or whitespace.", nameof(registrationNumber));}
            if (dailyRate <= 0){throw new ArgumentOutOfRangeException(nameof(dailyRate), $"{nameof(dailyRate)} cannot be less than or equal to zero.");}
            if(carId <= 0){throw new ArgumentOutOfRangeException(nameof(carId), $"{nameof(carId)} must be greater than zero.");}
            CarId = carId;
            RegistrationNumber = registrationNumber;
            DailyRate = dailyRate;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CarId;
            yield return RegistrationNumber;
            yield return DailyRate;
        }

    }

}
