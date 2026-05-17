using System;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Driver : Entity, IAggregateRoot
    {
        public string LicenceNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int FreeMinutes { get; private set; }

        private Driver()
        {
        }
        
        public Driver(string firstName, string lastName, string licenceNumber, int freeMinutes)
        {
            if (string.IsNullOrWhiteSpace(licenceNumber))
            {
            }
            if (freeMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(freeMinutes));
            }
            if(string.IsNullOrWhiteSpace(firstName)){throw new ArgumentException($"{nameof(firstName)} cannot be null or whitespace.", nameof(firstName));}
            if(string.IsNullOrWhiteSpace(lastName)){throw new ArgumentException($"{nameof(lastName)} cannot be null or whitespace.", nameof(lastName));}
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LicenceNumber = licenceNumber;
            this.FreeMinutes = freeMinutes;
        }

    }

}
