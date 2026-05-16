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

        public Rental(Renter renter, RentedCar rentedCar, DateTime startedAt)
        {
            this.Renter = renter ?? throw new ArgumentNullException(nameof(renter));
            this.RentedCar = rentedCar ?? throw new ArgumentNullException(nameof(rentedCar));
            this.StartedAt = startedAt;
            this.TotalAmount = 0;
            this.Total = Money.Zero;
        }

    }
}
