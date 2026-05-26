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

        //wstrzykuję przez kontruktor jednostkę pracy i serwis domenowy
        public CommandHandler(ICarRentalUnitOfWork uow, RentalService rentalService, IPositionService positionService)
        {
            this.uow = uow;
            this.rentalService = rentalService;
            this.positionService = positionService;
        }

        //przeładowania Execute:

        public void Execute(CreateCarCommand command)
        {
            //agregaty
            var position = new Position(command.PositionX, command.PositionY, command.PositionUnit);
            var car = new Car(command.RegistrationNumber, command.DailyRate, position);

            //zapis w repo
            uow.CarRepository.Insert(car);

            //zatwierdzenie transakcji
            uow.Commit();
        }

        public void Execute(CreateDriverCommand command)
        {
            //obiekty, które nie są agregatami, można tworzyć bezpośrednio w handlerze, bo nie mają swojej tożsamości i nie są zarządzane przez repozytorium
            var driver = new Driver(command.FirstName, command.LastName, command.LicenseNumber, command.FreeMinutes);
            uow.DriverRepository.Insert(driver); //dlatego insert, a nie add
            uow.Commit();
        }

        public void Execute(RentCarCommand command)
        {
            var car = uow.CarRepository.Get(command.CarId);
            var driver = uow.DriverRepository.Get(command.DriverId);
            //tworze obiekt klasy Rental
            var rental = rentalService.RentCar(driver, car, command.StartedAt);
            //to wywołuje car.Rent() i tworzy Rental przez fabrykę, ale nie zapisuje go nigdzie, bo to jest serwis domenowy, a nie repozytorium

            //zapis w repo
            uow.RentalRepository.Insert(rental);

            //zatwierdzenie transakcji
            uow.Commit();
        }

        public void Execute(ReturnCarCommand command)
        {
            var rental = uow.RentalRepository.Get(command.RentalId);
            var car = uow.CarRepository.Get(rental.RentedCar.CarId);
            var driver = uow.DriverRepository.Get(rental.Renter.DriverId);
            //nowa pozycja z serwisu infrastruktury (losowana)
            var position = positionService.GetCurrentPosition(car.Id);
            car.ChangePosition(position);

            rentalService.FinishRental(rental, car, command.FinishedAt); //wywołuje rental.Finish() i car.Return()
            //obliczenie minut darmowych:
            int freeMinutes = rental.CalculateBonusMinutes();
            if (freeMinutes > 0)
            {
                driver.AddFreeMinutes(freeMinutes, rental.FreeMinutesPolicy);
            }
            
            //zatwierdzenie transakcji
            uow.Commit();
        }
    }
}
