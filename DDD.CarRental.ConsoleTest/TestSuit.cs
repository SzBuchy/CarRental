using DDD.CarRental.Core.ApplicationLayer.Commands;
using DDD.CarRental.Core.ApplicationLayer.Commands.Handlers;
using DDD.CarRental.Core.ApplicationLayer.Queries;
using DDD.CarRental.Core.ApplicationLayer.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.CarRental.ConsoleTest
{
    public class TestSuit
    {
        private IServiceProvider _serviceProvide;

        private CommandHandler _commandHandler;
        private QueryHandler _queryHandler;

        public TestSuit(IServiceCollection serviceCollection)
        {
            _serviceProvide = serviceCollection.BuildServiceProvider();

            _commandHandler = _serviceProvide.GetRequiredService<CommandHandler>();
            _queryHandler = _serviceProvide.GetRequiredService<QueryHandler>();
        }

        public void Run()
        {
            Console.WriteLine("=== CarRental E2E scenario ===");

            _commandHandler.Execute(new CreateCarCommand
            {
                RegistrationNumber = "KR12345",
                DailyRate = 100,
                PositionX = 10,
                PositionY = 20,
                PositionUnit = "km"
            });

            _commandHandler.Execute(new CreateDriverCommand
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                LicenseNumber = "ABC123456",
                FreeMinutes = 0
            });

            var cars = _queryHandler.Execute(new GetAllCarsQuery());
            var drivers = _queryHandler.Execute(new GetAllDriversQuery());
            var car = cars.Last();
            var driver = drivers.Last();

            Console.WriteLine($"Utworzono samochód: {car.Id}, {car.RegistrationNumber}, status: {car.Status}");
            Console.WriteLine($"Utworzono kierowcę: {driver.Id}, {driver.FullName}, darmowe minuty: {driver.FreeMinutes}");

            _commandHandler.Execute(new RentCarCommand
            {
                CarId = (int)car.Id,
                DriverId = (int)driver.Id,
                StartedAt = DateTime.Now.AddMinutes(-42)
            });

            var rental = _queryHandler.Execute(new GetAllRentalsQuery()).Last();
            Console.WriteLine($"Wynajęto samochód. RentalId: {rental.Id}, kierowca: {rental.DriverName}, auto: {rental.RegistrationNumber}");

            _commandHandler.Execute(new ReturnCarCommand
            {
                RentalId = (int)rental.Id,
                FinishedAt = DateTime.Now
            });

            var returnedCar = _queryHandler.Execute(new GetAllCarsQuery()).Single(c => c.Id == car.Id);
            var updatedDriver = _queryHandler.Execute(new GetAllDriversQuery()).Single(d => d.Id == driver.Id);
            var finishedRental = _queryHandler.Execute(new GetAllRentalsQuery()).Single(r => r.Id == rental.Id);

            Console.WriteLine($"Zwrócono samochód. Status: {returnedCar.Status}");
            Console.WriteLine($"Nowa pozycja samochodu: X={returnedCar.PositionX}, Y={returnedCar.PositionY} {returnedCar.PositionUnit}");
            Console.WriteLine($"Koszt wynajmu: {finishedRental.TotalAmount:F2}");
            Console.WriteLine($"Darmowe minuty kierowcy po zwrocie: {updatedDriver.FreeMinutes}");
            Console.WriteLine("=== End ===");
        }
    }
}
