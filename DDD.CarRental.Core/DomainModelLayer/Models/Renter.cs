﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Renter : ValueObject
    {
        public long DriverId { get; private set; }
        public string LicenceNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Renter(long driverId, string licenceNumber, string firstName, string lastName)
        {
            DriverId = driverId;
            LicenceNumber = licenceNumber;
            FirstName = firstName;
            LastName = lastName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DriverId;
            yield return LicenceNumber;
            yield return FirstName;
            yield return LastName;
        }

    }

}
