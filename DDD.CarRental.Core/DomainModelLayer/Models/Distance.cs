﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Distance: ValueObject
    {
        public decimal Value { get; private set; }
        public string Unit { get; private set; }
        private const decimal MilesToKilometers = 1.609344m;

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
            Unit = NormalizeUnit(unit);
        }
        
        public Distance ConvertTo(string targetUnit)
        {
            targetUnit = NormalizeUnit(targetUnit);

            if (Unit == targetUnit)
            {
                return new Distance(Value, Unit);
            }

            return targetUnit switch
            {
                "km" => new Distance(Value *MilesToKilometers, "km"),
                "mi" => new Distance(Value / MilesToKilometers, "mi"),
                _ => throw new ArgumentException($"Unsupported unit: {targetUnit}", nameof(targetUnit))
            };
        }
        
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Unit;
        }
        private static string NormalizeUnit(string unit)
        {
            return unit.Trim().ToLowerInvariant() switch
            {
                "km" or "kilometer" or "kilometers" => "km",
                "mi" or "mile" or "miles" => "mi",
                _ => throw new ArgumentException($"Unsupported unit: {unit}", nameof(unit))
            };
        }
        
        public Distance Add(Distance other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var converted = other.ConvertTo(Unit);
            return new Distance(Value + converted.Value, Unit);
        }
        public Distance Subtract(Distance other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var converted = other.ConvertTo(Unit);
            var result = Value - converted.Value;

            if (result < 0)
            {
                throw new InvalidOperationException("Distance cannot be negative.");
            }

            return new Distance(result, Unit);
        }

      
        public int CompareTo(Distance other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var converted = other.ConvertTo(Unit);
            return Value.CompareTo(converted.Value);
        }
    }

}
