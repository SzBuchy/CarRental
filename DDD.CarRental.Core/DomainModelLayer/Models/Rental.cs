using System;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.CarRental.Core.DomainModelLayer.Policies;
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
        public FreeMinutesPolicy FreeMinutesPolicy { get; private set; }

        private Rental()
        {
        }

        public Rental(Renter renter, RentedCar rentedCar, DateTime startedAt, FreeMinutesPolicy freeMinutesPolicy)
        {
            this.Renter = renter ?? throw new ArgumentNullException(nameof(renter));
            this.RentedCar = rentedCar ?? throw new ArgumentNullException(nameof(rentedCar));
            this.FreeMinutesPolicy = freeMinutesPolicy ?? throw new ArgumentNullException(nameof(freeMinutesPolicy));
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
            this.Finish(finishedAt, new RentalPricingPolicy());
        }

        public void Finish(DateTime finishedAt, RentalPricingPolicy pricingPolicy)
        {
            if (this.FinishedAt != null)
            {
                throw new InvalidOperationException("Rental is already finished.");
            }

            if (pricingPolicy == null)
            {
                throw new ArgumentNullException(nameof(pricingPolicy));
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
            this.TotalAmount = this.CalculateTotalAmount(pricingPolicy);
            this.Total = new Money(this.TotalAmount);
            this.AddDomainEvent(new RentalFinishedEvent(this.Id, this.StartedAt, finishedAt, this.TotalAmount));
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
            return this.CalculateTotalAmount(new RentalPricingPolicy());
        }

        public decimal CalculateTotalAmount(RentalPricingPolicy pricingPolicy)
        {
            if (pricingPolicy == null)
            {
                throw new ArgumentNullException(nameof(pricingPolicy));
            }

            if (this.FinishedAt == null)
            {
                return 0;
            }

            return pricingPolicy.CalculateTotalAmount(this.StartedAt, this.FinishedAt.Value, this.RentedCar.DailyRate);
        }

        public Money CalculateTotal()
        {
            return new Money(this.CalculateTotalAmount());
        }

        public int CalculateBonusMinutes()
        {
            if (this.FinishedAt == null)
            {
                return 0;
            }

            return this.FreeMinutesPolicy.CalculateBonusMinutes(this.GetDuration(this.FinishedAt.Value));
        }
    }
}
