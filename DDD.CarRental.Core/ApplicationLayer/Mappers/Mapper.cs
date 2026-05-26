using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.ApplicationLayer.Mappers
{
    public class Mapper
    {
        public CarDTO MapToCarDto(Car car)
        {
            if (car == null) { return null; }

            return new CarDTO
            {
                Id = car.Id,
                RegistrationNumber = car.RegistrationNumber,
                DailyRate = car.DailyRate,
                Status = car.Status.ToString(),

                PositionX = car.CurrentPosition.X,
                PositionY = car.CurrentPosition.Y,
                PositionUnit = car.CurrentPosition.Unit ?? "",

                CurrentDistanceValue = car.CurrentDistance.Value,
                CurrentDistanceUnit = car.CurrentDistance.Unit ?? "",

                TotalDistanceValue = car.TotalDistance.Value,
                TotalDistanceUnit = car.TotalDistance.Unit ?? ""
            };
            
        }

        public DriverDTO MapToDriverDto(Driver driver)
        {
            if (driver == null) return null;

            return new DriverDTO
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                LicenceNumber = driver.LicenceNumber,
                FreeMinutes = driver.FreeMinutes
            };
        }

        public RentalDTO MapToRentalDto(Rental rental)
        {
            if (rental == null) return null;

            return new RentalDTO
            {
                Id = rental.Id,
                StartedAt = rental.StartedAt,
                FinishedAt = rental.FinishedAt,
                TotalAmount = rental.TotalAmount,
                IsFinished = rental.IsFinished(),

                //dane z VO Renter
                DriverId = rental.Renter.DriverId,
                DriverName = $"{rental.Renter.FirstName} {rental.Renter.LastName}",
                LicenceNumber = rental.Renter.LicenceNumber,

                //dane z VO RentedCar
                CarId = rental.RentedCar.CarId,
                RegistrationNumber = rental.RentedCar.RegistrationNumber,
                DailyRate = rental.RentedCar.DailyRate
            };
        }
    }
}
