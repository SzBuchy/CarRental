using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreateCarCommand
    {
        public string RegistrationNumber { get; set; }
        public decimal DailyRate { get; set; }
        public decimal PositionX { get; set; }
        public decimal PositionY { get; set; }
        public string PositionUnit { get; set; }
    }
}
