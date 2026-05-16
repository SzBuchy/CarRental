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
        
        public Driver(string firstName, string lastName, string licenceNumber, int freeMinutes)
        {
            if (string.IsNullOrWhiteSpace(licenceNumber))
            {
                throw new ArgumentException("No license number");
            }
            if (freeMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(freeMinutes));
            }
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LicenceNumber = licenceNumber;
            this.FreeMinutes = freeMinutes;
        }

    }

}
