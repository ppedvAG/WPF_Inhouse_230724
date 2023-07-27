using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System.Collections;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.CheesyDrive.Logic.Core
{
    public class RentService
    {
        private readonly IRepository repository;

        public RentService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Car> GetAvailableCars()
        {
            // Get all cars from the repository
            var allCars = repository.Query<Car>();

            // Get all ongoing rentals
            var ongoingRentals = repository.Query<Rent>().Where(r => r.EndDate == null || r.EndDate > DateTime.Now);

            // Get the car IDs from ongoing rentals
            var unavailableCarIds = ongoingRentals.Select(r => r.Car.Id);

            // Return all cars that are not in the unavailableCarIds
            return allCars.Where(c => !unavailableCarIds.Contains(c.Id));
        }
    }
}