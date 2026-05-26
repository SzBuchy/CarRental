using System;

namespace DDD.CarRental.Core.DomainModelLayer.Policies
{
    public class RentalPricingPolicy
    {
        public decimal CalculateTotalAmount(DateTime startedAt, DateTime finishedAt, decimal dailyRate)
        {
            decimal minuteRate =  dailyRate / 24/60;
            if (startedAt == default)
            {
                throw new ArgumentException($"{nameof(startedAt)} is required.", nameof(startedAt));
            }

            if (finishedAt == default)
            {
                throw new ArgumentException($"{nameof(finishedAt)} is required.", nameof(finishedAt));
            }

            if (finishedAt < startedAt)
            {
                throw new ArgumentException($"{nameof(finishedAt)} cannot be earlier than {nameof(startedAt)}.", nameof(finishedAt));
            }

            if (dailyRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dailyRate), $"{nameof(dailyRate)} must be greater than zero.");
            }

            var minutes = (decimal)Math.Ceiling((finishedAt - startedAt).TotalMinutes);

            if (minutes <= 0)
                
            {
                minutes = 1;
            }

            return minutes * minuteRate;
        }
    }
}
