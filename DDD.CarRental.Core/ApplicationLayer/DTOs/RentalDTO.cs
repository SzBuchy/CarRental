using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class RentalDTO
    {
        public long Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsFinished { get; set; }

        //spłaszcam VO Renter
        public long DriverId { get; set; }
        public string LicenceNumber { get; set; }
        public string DriverName { get; set; }
        
        //spłaszczam VO RentedCar
        public long CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal DailyRate { get; set; }
    }
}
