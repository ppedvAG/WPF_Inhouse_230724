using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.CheesyDrive.Logic.Core.Tests
{
    [TestClass]
    public class RentServiceTests
    {
        [TestMethod]
        public void GetAvailableCars_with_three_cars()
        {
            var rs = new RentService(new TestRepo());

            var result = rs.GetAvailableCars();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAvailableCars_with_three_cars_moq()
        {
            // Arrange
            var availableCar1 = new Car { Id = 1, Manufacturer = "Toyota", Model = "Camry", KW = 150, Color = "Blue", EngineType = EngineType.Gas, Seats = 5, Plate = "ABC123" };
            var availableCar2 = new Car { Id = 2, Manufacturer = "Honda", Model = "Civic", KW = 120, Color = "Red", EngineType = EngineType.Gas, Seats = 4, Plate = "XYZ789" };
            var rentedCar = new Car { Id = 3, Manufacturer = "Ford", Model = "Focus", KW = 130, Color = "Black", EngineType = EngineType.Diesel, Seats = 5, Plate = "DEF456" };

            var rentals = new List<Rent>
                  {
                 new Rent { Id = 1, Car = rentedCar, BookDate = DateTime.Now.AddDays(-5), StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(2) },
                 new Rent { Id = 2, Car = rentedCar, BookDate = DateTime.Now.AddDays(-3), StartDate = DateTime.Now.AddDays(-1), EndDate = null }
                   };
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Car>()).Returns(() => new List<Car> { availableCar1, availableCar2, rentedCar }.AsQueryable());
            mock.Setup(x => x.Query<Rent>()).Returns(() => rentals.AsQueryable());

            var rs = new RentService(mock.Object);

            var result = rs.GetAvailableCars();

            Assert.AreEqual(2, result.Count());
            mock.Verify(x => x.Query<Car>(), Times.Exactly(1));
        }
    }

    public class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            var availableCar1 = new Car { Id = 1, Manufacturer = "Toyota", Model = "Camry", KW = 150, Color = "Blue", EngineType = EngineType.Gas, Seats = 5, Plate = "ABC123" };
            var availableCar2 = new Car { Id = 2, Manufacturer = "Honda", Model = "Civic", KW = 120, Color = "Red", EngineType = EngineType.Gas, Seats = 4, Plate = "XYZ789" };
            var rentedCar = new Car { Id = 3, Manufacturer = "Ford", Model = "Focus", KW = 130, Color = "Black", EngineType = EngineType.Diesel, Seats = 5, Plate = "DEF456" };

            var rentals = new List<Rent>
             {
                   new Rent { Id = 1, Car = rentedCar, BookDate = DateTime.Now.AddDays(-5), StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(2) },
                    new Rent { Id = 2, Car = rentedCar, BookDate = DateTime.Now.AddDays(-3), StartDate = DateTime.Now.AddDays(-1), EndDate = null }
             };
            if (typeof(T) == typeof(Car))
            {
                return new[] { availableCar1, availableCar2, rentedCar }.AsQueryable().Cast<T>();
            }

            if (typeof(T) == typeof(Rent))
            {
                return rentals.Cast<T>().AsQueryable();
            }
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
