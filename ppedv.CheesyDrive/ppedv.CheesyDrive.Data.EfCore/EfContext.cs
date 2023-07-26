using Microsoft.EntityFrameworkCore;
using ppedv.CheesyDrive.Model;

namespace ppedv.CheesyDrive.Data.EfCore
{
    public class EfContext : DbContext
    {
        private readonly string conString;
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rent> Rents { get; set; }

        //#if net461
        //public EfContext(string conString) : base(conString)
        //{

        //}
        //#endif

        public EfContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }
    }
}