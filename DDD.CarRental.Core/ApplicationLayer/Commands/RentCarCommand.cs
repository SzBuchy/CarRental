using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class RentCarCommand
    {
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartedAt { get; set; }
        
        //w Rental jest kontruktor:
        //public Rental(Renter renter, RentedCar rentedCar, DateTime startedAt)
        //więc mogę tu użyć tylko CarId i DriverId, a nie całych obiektów,
        //bo to jest warstwa aplikacji, a nie domeny
        //i znajdę te obiekty w handlerze
    }
}
