using ppedv.CheesyDrive.Model;
using System;
using System.Data.Entity;
using System.Reflection.Emit;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(p => p.HasColumnType("datetime2"));
        }

    }
}