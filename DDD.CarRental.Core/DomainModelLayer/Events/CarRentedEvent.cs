using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class CarRentedEvent : DomainEvent
    {
        public long CarId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public DateTime RentedAt { get; private set; }

        public CarRentedEvent(long carId, string registrationNumber, DateTime rentedAt)
        {
            CarId = carId;
            RegistrationNumber = registrationNumber;
            RentedAt = rentedAt;
        }
    }
}
