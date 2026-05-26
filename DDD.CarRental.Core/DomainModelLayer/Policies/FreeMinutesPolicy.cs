using System;

namespace DDD.CarRental.Core.DomainModelLayer.Policies
{
    public class FreeMinutesPolicy
    {
        public int CalculateBonusMinutes(TimeSpan duration)
        {
            if (duration.TotalMinutes < 0)
            {
                return 0;
            }

            // Za każdą pełną godzinę wykorzystaną dostajesz 5 darmowych minut
            int totalHours = (int)duration.TotalHours;
            return totalHours * 5;
        }

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
