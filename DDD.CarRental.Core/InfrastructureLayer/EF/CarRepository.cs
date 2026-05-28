using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Car Get(long id) => _context.Cars.Find(id);

        public IList<Car> GetAll() => _context.Cars.ToList();

        public IList<Car> Find(Expression<Func<Car, bool>> expression)
        {
            return _context.Cars.Where(expression).ToList();
        }

        public void Insert(Car car) => _context.Cars.Add(car);

        public void Delete(Car car) => _context.Cars.Remove(car);
    }
}
