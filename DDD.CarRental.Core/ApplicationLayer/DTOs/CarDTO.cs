using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class CarDTO
    {
        public long Id { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal DailyRate { get; set; }
        public string Status { get; set; }

        public decimal PositionX { get; set; }
        public decimal PositionY { get; set; }
        public string PositionUnit { get; set; }

        public decimal CurrentDistanceValue { get; set; }
        public string CurrentDistanceUnit { get; set; }

        public decimal TotalDistanceValue { get; set; }
        public string TotalDistanceUnit { get; set; }
    }
}
