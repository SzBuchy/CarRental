﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class RentedCar : ValueObject
    {
        public long CarId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public decimal DailyRate { get; private set; }

        public RentedCar(long carId, string registrationNumber, decimal dailyRate)
        {
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
