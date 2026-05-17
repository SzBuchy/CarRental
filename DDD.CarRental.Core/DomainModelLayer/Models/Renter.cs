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

        private Renter()
        {
        }

        public Renter(long driverId, string licenceNumber, string firstName, string lastName)
        {
            if (driverId <= 0){throw new ArgumentOutOfRangeException(nameof(driverId), $"{nameof(driverId)} must be greater than zero.");}

            if (string.IsNullOrWhiteSpace(licenceNumber))
            {
                throw new ArgumentException($"{nameof(licenceNumber)} cannot be null or whitespace.", nameof(licenceNumber));
            }
            if (string.IsNullOrWhiteSpace(firstName)){throw new ArgumentException($"{nameof(firstName)} cannot be null or whitespace.", nameof(firstName));}
            if(string.IsNullOrWhiteSpace(lastName)){throw new ArgumentException($"{nameof(lastName)} cannot be null or whitespace.", nameof(lastName));}
            
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
