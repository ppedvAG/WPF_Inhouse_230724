using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}