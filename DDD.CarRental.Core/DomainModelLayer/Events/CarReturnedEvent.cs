using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class CarReturnedEvent : DomainEvent
    {
        public long CarId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public DateTime ReturnedAt { get; private set; }

        public CarReturnedEvent(long carId, string registrationNumber, DateTime returnedAt)
        {
            CarId = carId;
            RegistrationNumber = registrationNumber;
            ReturnedAt = returnedAt;
        }
    }
}
