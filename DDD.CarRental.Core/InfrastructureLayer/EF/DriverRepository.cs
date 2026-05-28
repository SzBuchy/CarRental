using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class DriverRepository : IDriverRepository
    {
        private readonly CarRentalDbContext _context;

        public DriverRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Driver Get(long id) => _context.Drivers.Find(id);

        public IList<Driver> GetAll()
        {
            var connection = _context.Database.GetDbConnection();
            return connection.Query<Driver>("SELECT * FROM Drivers").ToList();
        }

        public IList<Driver> Find(Expression<Func<Driver, bool>> expression)
        {
            return _context.Drivers.Where(expression).ToList();
        }

        public void Insert(Driver driver) => _context.Drivers.Add(driver);

        public void Delete(Driver driver) => _context.Drivers.Remove(driver);
    }
}
