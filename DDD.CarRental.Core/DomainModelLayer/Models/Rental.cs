using System;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Rental : Entity, IAggregateRoot
    {
        public DateTime StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public RentedCar RentedCar { get; private set; }
        public Renter Renter { get; private set; }
        public Money Total { get; private set; }

        private Rental()
        {
        }

        public Rental(Renter renter, RentedCar rentedCar, DateTime startedAt)
        {
            this.Renter = renter ?? throw new ArgumentNullException(nameof(renter));
            this.RentedCar = rentedCar ?? throw new ArgumentNullException(nameof(rentedCar));
            if (startedAt == default)
            {
                throw new ArgumentException($"{nameof(startedAt)} is required.", nameof(startedAt));
            }
            this.StartedAt = startedAt;
            this.TotalAmount = 0;
            this.Total = Money.Zero;
        }

        public void Finish(DateTime finishedAt)
        {
            if (this.FinishedAt != null)
            {
                throw new InvalidOperationException("Rental is already finished.");
            }

            if (finishedAt == default)
            {
                throw new ArgumentException($"{nameof(finishedAt)} is required.", nameof(finishedAt));
            }

            if (finishedAt < this.StartedAt)
            {
                throw new ArgumentException($"{nameof(finishedAt)} cannot be earlier than {nameof(StartedAt)}.", nameof(finishedAt));
            }

            this.FinishedAt = finishedAt;
            this.TotalAmount = this.CalculateTotalAmount();
            this.Total = this.CalculateTotal();
        }

        public bool IsFinished()
        {
            return this.FinishedAt != null;
        }

        public bool IsActive()
        {
            return this.FinishedAt == null;
        }

        public TimeSpan GetDuration(DateTime now)
        {
            if (now < this.StartedAt)
            {
                throw new ArgumentException($"{nameof(now)} cannot be earlier than {nameof(StartedAt)}.", nameof(now));
            }

            var end = this.FinishedAt ?? now;
            return end - this.StartedAt;
        }

        public decimal CalculateTotalAmount()
        {
            if (this.FinishedAt == null)
            {
                return 0;
            }

            var days = (this.FinishedAt.Value.Date - this.StartedAt.Date).Days;

            if (days <= 0)
            {
                days = 1;
            }

            return days * this.RentedCar.DailyRate;
        }

        public Money CalculateTotal()
        {
            return new Money(this.CalculateTotalAmount());
        }
    }
}
