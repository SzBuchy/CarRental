using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarRentalUnitOfWork : ICarRentalUnitOfWork
    {
        private readonly CarRentalDbContext _context;
        private readonly IDomainEventPublisher _publisher;

        public ICarRepository CarRepository { get; }
        public IDriverRepository DriverRepository { get; }
        public IRentalRepository RentalRepository { get; }

        public CarRentalUnitOfWork(CarRentalDbContext context, IDomainEventPublisher publisher)
        {
            _context = context;
            _publisher = publisher;

            CarRepository = new CarRepository(context);
            DriverRepository = new DriverRepository(context);
            RentalRepository = new RentalRepository(context);
        }

        public void Commit()
        {
            var domainEvents = _context.ChangeTracker.Entries<Entity>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            _context.SaveChanges();

            foreach (var @event in domainEvents)
            {
                _publisher.Publish((dynamic)@event);
            }

            foreach (var entry in _context.ChangeTracker.Entries<Entity>())
            {
                entry.Entity.RemoveAllDomainEvents();
            }
        }

        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        break;
                }
            }
        }

        public void Dispose() => _context.Dispose();
    }
}
