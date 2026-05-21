using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.ApplicationLayer.Mappers;
using DDD.CarRental.Core.ApplicationLayer.DTOs;
using System.Linq;

namespace DDD.CarRental.Core.ApplicationLayer.Queries.Handlers
{
    public class QueryHandler
    {
        //zwraca DTO przez Mapper i jest super
        private readonly ICarRentalUnitOfWork cruow;
        private readonly Mapper mapper;

        public QueryHandler(ICarRentalUnitOfWork cruow, Mapper mapper)
        {
            this.cruow = cruow;
            this.mapper = mapper;
        }

        //tak jak w CommandHandlerze

        public CarDTO Execute(GetCarQuery query)
        {
            var car = cruow.CarRepository.Get(query.CarId);
            return mapper.MapToCarDto(car);
        }

        public IList<CarDTO> Execute(GetAllCarsQuery query)
        {
            var cars = cruow.CarRepository.GetAll();
            return cars.Select(car => mapper.MapToCarDto(car)).ToList();
        }


        public DriverDTO Execute(GetDriverQuery query)
        {
            var driver = cruow.DriverRepository.Get(query.DriverId);
            return mapper.MapToDriverDto(driver);
        }

        public IList<DriverDTO> Execute(GetAllDriversQuery query)
        {
            var drivers = cruow.DriverRepository.GetAll();
            return drivers.Select(d => mapper.MapToDriverDto(d)).ToList();
        }


        public RentalDTO Execute(GetRentalQuery query)
        {
            var rental = cruow.RentalRepository.Get(query.RentalId);
            return mapper.MapToRentalDto(rental);
        }

        public IList<RentalDTO> Execute(GetAllRentalsQuery query)
        {
            var rentals = cruow.RentalRepository.GetAll();
            return rentals.Select(r => mapper.MapToRentalDto(r)).ToList();
        }


    }
}
