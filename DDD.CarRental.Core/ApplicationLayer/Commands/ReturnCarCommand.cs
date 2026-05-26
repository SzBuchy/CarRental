using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class ReturnCarCommand
    {
        public int RentalId { get; set; }
        public DateTime FinishedAt { get; set; }

        //mam tylko RentalId, bo to jest warstwa aplikacji, a nie domeny,
        //więc nie mogę użyć całego obiektu Rental, tylko jego Id,
        //a w handlerze znajdę ten obiekt i wywołam na nim metodę Finish
    }
}
