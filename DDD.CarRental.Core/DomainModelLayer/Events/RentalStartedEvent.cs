using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class RentalStartedEvent : DomainEvent
    {
        public long RentalId { get; private set; }
        public long DriverId { get; private set; }
        public long CarId { get; private set; }
        public DateTime StartedAt { get; private set; }

        public RentalStartedEvent(long rentalId, long driverId, long carId, DateTime startedAt)
        {
            RentalId = rentalId;
            DriverId = driverId;
            CarId = carId;
            StartedAt = startedAt;
        }
    }
}
