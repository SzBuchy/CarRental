using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.DomainModelLayer.Policies;
using DDD.CarRental.Core.DomainModelLayer.Services;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private readonly ICarRentalUnitOfWork uow;
        private readonly RentalService rentalService;
        private readonly IPositionService positionService;
public CommandHandler(ICarRentalUnitOfWork uow, RentalService rentalService, IPositionService positionService)
{
    this.uow = uow;
    this.rentalService = rentalService;
    this.positionService = positionService;
}

public void Execute(CreateCarCommand command)
        {
            var position = new Position(command.PositionX, command.PositionY, command.PositionUnit);
            var car = new Car(command.RegistrationNumber, command.DailyRate, position);

            uow.CarRepository.Insert(car);
            uow.Commit();
        }

        public void Execute(CreateDriverCommand command)
        {
            var driver = new Driver(command.FirstName, command.LastName, command.LicenseNumber, command.FreeMinutes);
            uow.DriverRepository.Insert(driver);
            uow.Commit();
        }

        public void Execute(RentCarCommand command)
        {
            var car = uow.CarRepository.Get(command.CarId);
            var driver = uow.DriverRepository.Get(command.DriverId);
            var rental = rentalService.RentCar(driver, car, command.StartedAt);

            uow.RentalRepository.Insert(rental);
            uow.Commit();
        }

        public void Execute(ReturnCarCommand command)
        {
            var rental = uow.RentalRepository.Get(command.RentalId);
            var car = uow.CarRepository.Get(rental.RentedCar.CarId);
            var driver = uow.DriverRepository.Get(rental.Renter.DriverId);
            var position = positionService.GetCurrentPosition(car.Id);
            car.ChangePosition(position);

            rentalService.FinishRental(rental, car, command.FinishedAt);
            int freeMinutes = rental.CalculateBonusMinutes();
            if (freeMinutes > 0)
            {
                driver.AddFreeMinutes(freeMinutes, rental.FreeMinutesPolicy);
            }
            
            uow.Commit();
        }
    }
}
