using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events
{
    public class RentalFinishedEvent : DomainEvent
    {
        public long RentalId { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public decimal TotalAmount { get; private set; }

        public RentalFinishedEvent(long rentalId, DateTime startedAt, DateTime finishedAt, decimal totalAmount)
        {
            RentalId = rentalId;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            TotalAmount = totalAmount;
        }
    }
}
