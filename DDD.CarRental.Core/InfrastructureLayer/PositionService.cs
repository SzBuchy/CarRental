using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.InfrastructureLayer
{
    public class PositionService : IPositionService
    {
        public Position GetCurrentPosition(long carId)
        {
            var random = new Random();
            return new Position(
                random.Next(-100, 100),
                random.Next(-100, 100),
                "km"
            );
        }
    }
}
