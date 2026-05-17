﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Distance: ValueObject
    {
        public decimal Value { get; private set; }
        public string Unit { get; private set; }

        private Distance()
        {
        }

        public Distance(decimal value, string unit)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} must be greater than or equal to zero.");
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException($"{nameof(unit)} cannot be null or whitespace.", nameof(unit));
            }
            Value = value;
            Unit = unit;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Unit;
        }

    }

}
