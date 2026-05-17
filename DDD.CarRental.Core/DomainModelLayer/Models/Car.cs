using System;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Car : Entity, IAggregateRoot
    {
        public string RegistrationNumber { get; private set; }
        public decimal DailyRate { get; private set; }
        public Position CurrentPosition { get; private set; }
        public Distance TotalDistance { get; private set; }
        public Distance CurrentDistance { get; private set; }
        public CarStatus Status { get; private set; }

        private Car()
        {
        }

        public Car(string registrationNumber, decimal dailyRate, Position currentPosition)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber) || dailyRate <= 0 || currentPosition == null)
            {
                throw new Exception($"Incorrect data");
            }
            this.RegistrationNumber = registrationNumber;
            this.DailyRate = dailyRate;
            this.CurrentPosition = currentPosition;
            this.TotalDistance = new Distance(0, currentPosition.Unit);
            this.CurrentDistance = new Distance(0, currentPosition.Unit);
            this.Status = CarStatus.Free;
        }

        public void Rent()
        {
            if (this.Status == CarStatus.Rented)
            {
                throw new InvalidOperationException($"Car '{this.RegistrationNumber}' is not free.");
            }

            this.Status = CarStatus.Rented;

        }

        public void Return()
        {
            if (this.Status == CarStatus.Free)
            {
                throw new InvalidOperationException($"Car '{this.RegistrationNumber}' was not rented.");
            }
            this.Status = CarStatus.Free;
        }

        public void ChangePosition(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            this.CurrentPosition = position;
        }

        public void ChangeDailyRate(decimal dailyRate)
        {
            if (dailyRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dailyRate));
            }
            this.DailyRate = dailyRate;
        }

        public void ChangeCurrentDistance(Distance currentDistance)
        {
            this.CurrentDistance = currentDistance ?? throw new ArgumentNullException(nameof(currentDistance));
        }

        public void ChangeTotalDistance(Distance totalDistance)
        {
            this.TotalDistance = totalDistance ?? throw new ArgumentNullException(nameof(totalDistance));
        }

    }

}
