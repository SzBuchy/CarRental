using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IPositionService
    {
        Position GetCurrentPosition(long carId);
    }
}
