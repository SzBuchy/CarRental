using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.SharedKernel.ApplicationLayer;

namespace DDD.CarRental.Core.ApplicationLayer.DomainEventHandlers
{
    public class RentalFinishedEventHandler : IEventHandler<RentalFinishedEvent>
    {
        public void Handle(RentalFinishedEvent domainEvent)
        {
            //wysatrczy że coś wypiszę na potrzeby projektu
            Console.WriteLine($"[EVENT] Wypożyczenie {domainEvent.RentalId}, utworzone o czasie {domainEvent.StartedAt}, zakończono o czasie {domainEvent.FinishedAt}. Kwota do zapłacenia wynosi {domainEvent.TotalAmount:F2}");
        }
    }
}
