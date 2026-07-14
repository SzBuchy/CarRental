using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalDbContext _context;

        public RentalRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Rental Get(long id) => _context.Rentals.Find(id);

        public IList<Rental> GetAll() => _context.Rentals.ToList();

        public IList<Rental> Find(Expression<Func<Rental, bool>> expression)
        {
            return _context.Rentals.Where(expression).ToList();
        }

        public void Insert(Rental rental) => _context.Rentals.Add(rental);

        public void Delete(Rental rental) => _context.Rentals.Remove(rental);
    }
}
