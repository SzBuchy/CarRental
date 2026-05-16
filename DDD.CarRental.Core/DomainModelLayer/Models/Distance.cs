﻿using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Distance: ValueObject
    {
        public decimal Value { get; private set; }
        public string Unit { get; private set; }

        public Distance(decimal value, string unit)
        {
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
