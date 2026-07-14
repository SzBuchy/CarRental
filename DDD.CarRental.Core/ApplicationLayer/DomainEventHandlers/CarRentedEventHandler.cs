using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.SharedKernel.ApplicationLayer;
using DDD.CarRental.Core.DomainModelLayer.Events;

namespace DDD.CarRental.Core.ApplicationLayer.DomainEventHandlers
{
    public class CarRentedEventHandler : IEventHandler<CarRentedEvent>
    {
        public void Handle(CarRentedEvent domainEvent)
        {
            Console.WriteLine($"[EVENT] Auto {domainEvent.CarId}, o numerze rejestracyjnym {domainEvent.RegistrationNumber} wynajęte o czasie {domainEvent.RentedAt}!");
        }
    }
}
