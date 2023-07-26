using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.CheesyDrive.Model;

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

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Can_create_car()
        {
            var car = new Car() { Color = "gelb", KW = 56 };
            var con = new EfContext(conString);
            con.Database.CreateIfNotExists();

            con.Cars.Add(car);
            var rows = con.SaveChanges();

            Assert.AreEqual(1, rows);
        }
    }
}