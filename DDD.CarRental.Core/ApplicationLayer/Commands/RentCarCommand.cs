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
    }
}
