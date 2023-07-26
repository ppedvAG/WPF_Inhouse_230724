using ppedv.CheesyDrive.Model;
using System.Data.Entity;

namespace ppedv.CheesyDrive.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public EfContext(string conString) : base(conString)
        {
        }

    }
}