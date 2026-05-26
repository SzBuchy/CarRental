using DDD.CarRental.Core.ApplicationLayer.Commands.Handlers;
using DDD.CarRental.Core.ApplicationLayer.DomainEventHandlers;
using DDD.CarRental.Core.ApplicationLayer.Mappers;
using DDD.CarRental.Core.ApplicationLayer.Queries.Handlers;
using DDD.CarRental.Core.DomainModelLayer.Events;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Services;
using DDD.CarRental.Core.InfrastructureLayer;
using DDD.CarRental.Core.InfrastructureLayer.EF;
using DDD.SharedKernel.ApplicationLayer;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.InfrastructureLayer.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.CarRental.ConsoleTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlite("Data Source=car-rental-test.db"));

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<ICarRentalUnitOfWork, CarRentalUnitOfWork>();

            services.AddScoped<IDomainEventPublisher, SimpleEventPublisher>();
            services.AddScoped<IEventHandler<CarRentedEvent>, CarRentedEventHandler>();
            services.AddScoped<IEventHandler<CarReturnedEvent>, CarReturnedEventHandler>();
            services.AddScoped<IEventHandler<RentalStartedEvent>, RentalStartedEventHandler>();
            services.AddScoped<IEventHandler<RentalFinishedEvent>, RentalFinishedEventHandler>();

            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<RentalService>();
            services.AddScoped<Mapper>();
            services.AddScoped<CommandHandler>();
            services.AddScoped<QueryHandler>();

            new TestSuit(services).Run();
        }
    }
}
