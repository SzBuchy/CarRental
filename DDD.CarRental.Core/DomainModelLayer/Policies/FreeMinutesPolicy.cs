using System;

namespace DDD.CarRental.Core.DomainModelLayer.Policies
{
    public class FreeMinutesPolicy
    {
        public void CheckCanAdd(int minutes)
        {
            if (minutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), $"{nameof(minutes)} must be greater than zero.");
            }
        }

        public void CheckCanUse(int currentFreeMinutes, int minutes)
        {
            if (currentFreeMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(currentFreeMinutes), $"{nameof(currentFreeMinutes)} cannot be less than zero.");
            }

            if (minutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), $"{nameof(minutes)} must be greater than zero.");
            }

            if (minutes > currentFreeMinutes)
            {
                throw new InvalidOperationException("Not enough free minutes.");
            }
        }

        public bool HasFreeMinutes(int currentFreeMinutes)
        {
            if (currentFreeMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(currentFreeMinutes), $"{nameof(currentFreeMinutes)} cannot be less than zero.");
            }

            return currentFreeMinutes > 0;
        }
    }
}
