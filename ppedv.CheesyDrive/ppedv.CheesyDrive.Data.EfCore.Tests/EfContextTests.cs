using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.CheesyDrive.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ppedv.CheesyDrive.Data.EfCore.Tests
{
    [TestClass]
    public class EfContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=CheesyDrive_Test;Trusted_Connection=true";

        [TestMethod]
        public void Can_create_db()
        {
            var con = new EfContext(conString);
            if (con.Database.Exists())
                con.Database.Delete();

            var result = con.Database.CreateIfNotExists();

            //Assert.IsTrue(result);
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Can_create_car()
        {
            var car = new Car() { Color = "gelb", KW = 56 };
            var con = new EfContext(conString);
            con.Database.CreateIfNotExists();

            con.Cars.Add(car);
            var rows = con.SaveChanges();

            //Assert.AreEqual(1, rows);
            rows.Should().Be(1);
        }

        [TestMethod]
        public void Can_read_car()
        {
            var car = new Car() { Model = $"L7_{Guid.NewGuid()}", Color = "gelb", KW = 56 };

            using (var con = new EfContext(conString))
            {
                con.Database.CreateIfNotExists();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                //Assert.AreEqual(car.Model, loaded.Model);
                loaded.Model.Should().Be(car.Model);
            }
        }


        [TestMethod]
        public void Can_update_car()
        {
            var car = new Car() { Model = $"L7_{Guid.NewGuid()}", Color = "gelb", KW = 56 };
            var newModel = $"P9_{Guid.NewGuid()}";

            using (var con = new EfContext(conString))
            {
                con.Database.CreateIfNotExists();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                loaded.Model = newModel;
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                //Assert.AreEqual(newModel, loaded.Model);
                loaded.Model.Should().Be(newModel);
            }
        }

        [TestMethod]
        public void Can_delete_car()
        {
            var car = new Car() { Model = $"L7_{Guid.NewGuid()}", Color = "gelb", KW = 56 };

            using (var con = new EfContext(conString))
            {
                con.Database.CreateIfNotExists();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                con.Cars.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                //Assert.IsNull(loaded);
                loaded.Should().BeNull();  
            }
        }

        [TestMethod]
        public void Can_insert_car_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter("Id"));
            var car = fix.Build<Car>().Do(x => x.EngineType = EngineType.Electric)
                                      .Without(x => x.Seats)
                                      .Create();

            car.EngineType = EngineType.Hydro;

            using (var con = new EfContext(conString))
            {
                con.Database.CreateIfNotExists();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);
                loaded.Should().BeEquivalentTo(car, x => x.IgnoringCyclicReferences());
            }
        }

    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}