using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.SharedKernel.ApplicationLayer;

namespace DDD.CarRental.Core.ApplicationLayer.DomainEventHandlers
{
    public class CarReturnedEventHandler : IEventHandler<CarReturnedEvent>
    {
        public void Handle(CarReturnedEvent domainEvent)
        {
            //analogicznie jak w przypadku CarRentedEventHandler, tutaj również wystarczy że coś wypiszę
            Console.WriteLine($"[EVENT] Auto {domainEvent.CarId}, o numerze rejestracyjnym {domainEvent.RegistrationNumber} zwrócone o czasie {domainEvent.ReturnedAt}!");
        }
    }
}
