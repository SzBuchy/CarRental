﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Position : ValueObject
    {
        public decimal X { get; private set; }
        public decimal Y { get; private set; }
        public string Unit { get; private set; }

        private Position()
        {
        }

        public Position(decimal x, decimal y, string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException($"{nameof(unit)} cannot be null or whitespace.", nameof(unit));
            }
            X = x;
            Y = y;
            Unit = unit;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
            yield return Unit;
        }

        public Distance Distance(Position other)
        {
            var deltaX = X - other.X;
            var deltaY = Y - other.Y;

            var distance = (decimal)Math.Sqrt(
                Math.Pow((double)deltaX, 2) + Math.Pow((double)deltaY, 2)
            );
            return new Distance(distance, "km");
        }
    }

}
