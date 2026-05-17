using System;
using DDD.CarRental.Core.DomainModelLayer.Policies;
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
                throw new ArgumentException($"{nameof(licenceNumber)} cannot be null or whitespace.", nameof(licenceNumber));
            }
            if (freeMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(freeMinutes));
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException($"{nameof(firstName)} cannot be null or whitespace.", nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException($"{nameof(lastName)} cannot be null or whitespace.", nameof(lastName));
            }
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LicenceNumber = licenceNumber;
            this.FreeMinutes = freeMinutes;
        }

        public void AddFreeMinutes(int minutes)
        {
            this.AddFreeMinutes(minutes, new FreeMinutesPolicy());
        }

        public void AddFreeMinutes(int minutes, FreeMinutesPolicy freeMinutesPolicy)
        {
            if (freeMinutesPolicy == null)
            {
                throw new ArgumentNullException(nameof(freeMinutesPolicy));
            }

            freeMinutesPolicy.CheckCanAdd(minutes);

            this.FreeMinutes += minutes;
        }

        public void UseFreeMinutes(int minutes)
        {
            this.UseFreeMinutes(minutes, new FreeMinutesPolicy());
        }

        public void UseFreeMinutes(int minutes, FreeMinutesPolicy freeMinutesPolicy)
        {
            if (freeMinutesPolicy == null)
            {
                throw new ArgumentNullException(nameof(freeMinutesPolicy));
            }

            freeMinutesPolicy.CheckCanUse(this.FreeMinutes, minutes);

            this.FreeMinutes -= minutes;
        }

        public bool HasFreeMinutes()
        {
            return this.HasFreeMinutes(new FreeMinutesPolicy());
        }

        public bool HasFreeMinutes(FreeMinutesPolicy freeMinutesPolicy)
        {
            if (freeMinutesPolicy == null)
            {
                throw new ArgumentNullException(nameof(freeMinutesPolicy));
            }

            return freeMinutesPolicy.HasFreeMinutes(this.FreeMinutes);
        }

    }

}
