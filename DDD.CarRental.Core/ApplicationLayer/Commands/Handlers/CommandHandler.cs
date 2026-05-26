using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.CarRental.Core.DomainModelLayer.Services;
using DDD.CarRental.Core.InfrastructureLayer;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private readonly ICarRentalUnitOfWork uow;
        private readonly RentalService rentalService;
        private readonly PositionService positionService;

        //wstrzykuję przez kontruktor jednostkę pracy i serwis domenowy
        public CommandHandler(ICarRentalUnitOfWork uow, RentalService rentalService)
        {
            this.uow = uow;
            this.rentalService = rentalService;
        }

        //przeładowania Execute:

        public void Execute(CreateCarCommand command)
        {
            //agregaty
            var position = new Position(command.PositionX, command.PositionY, command.PositionUnit);
            var car = new Car(command.RegistrationNumber, command.DailyRate, position);

            //zapis w repo
            uow.CarRepository.Add(car);

            //zatwierdzenie transakcji
            uow.Commit();
        }

        public void Execute(CreateDriverCommand command)
        {
            //obiekty, które nie są agregatami, można tworzyć bezpośrednio w handlerze, bo nie mają swojej tożsamości i nie są zarządzane przez repozytorium
            var driver = new Driver(command.LicenseNumber, command.FirstName, command.LastName, command.FreeMinutes);
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

            rentalService.FinishRental(rental, car, DateTime.Now); //wywołuje rental.Finish() i car.Return()
            //obliczenie minut darmowych:
            int freeMinutes = (int)rental.GetDuration(DateTime.Now).TotalMinutes;
            driver.AddFreeMinutes(freeMinutes);
            
            //zatwierdzenie transakcji
            uow.Commit();
        }
}
