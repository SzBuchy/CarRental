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

        public Position(decimal x, decimal y, string unit)
        {
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

    }

}
