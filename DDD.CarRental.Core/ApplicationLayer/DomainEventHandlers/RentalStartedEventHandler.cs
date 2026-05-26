using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.SharedKernel.ApplicationLayer;

namespace DDD.CarRental.Core.ApplicationLayer.DomainEventHandlers
{
    public class RentalStartedEventHandler : IEventHandler<RentalStartedEvent>
    {
        public void Handle(RentalStartedEvent domainEvent)
        {
            //wysatrczy że coś wypiszę na potrzeby projektu
            Console.WriteLine($"[EVENT] Wypożyczenie {domainEvent.RentalId}, samochodu {domainEvent.CarId}, przez kierowcę {domainEvent.DriverId} utworzone o czasie {domainEvent.StartedAt}!");
        }
    }
}
