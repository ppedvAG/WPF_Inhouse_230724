using Autofac;
using ppedv.CheesyDrive.Data.EfCore;
using ppedv.CheesyDrive.Logic.Core;
using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.CheesyDrive.UI.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Cheesy Car v0.1 GOLD EDITION ***");

            string conString = "Server=(localdb)\\mssqllocaldb;Database=CheesyDrive_Test;Trusted_Connection=true";

            //manual injection
            //var repo = new EfRepository(conString);

            //injection per reflection
            //var path = @"C:\Users\ar2\source\repos\WPF_Inhouse_230724\ppedv.CheesyDrive\ppedv.CheesyDrive.Data.EfCore\bin\Debug\net48\ppedv.CheesyDrive.Data.EfCore.dll";
            //var ass = Assembly.LoadFrom(path);
            //var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
            //var repo = (IRepository)Activator.CreateInstance(typeMitRepo,conString);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(x => new EfRepository(conString)).As<IRepository>();
            var container = containerBuilder.Build();

            var repo = container.Resolve<IRepository>();   
            var rentService = new RentService(repo);


            foreach (var car in repo.GetAll<Car>())
            {
                Console.WriteLine($"{car.Manufacturer} {car.Model}");
            }

            Console.WriteLine("==== AVAILABLE =====");

            foreach (var car in rentService.GetAvailableCars())
            {
                Console.WriteLine($"{car.Manufacturer} {car.Model}");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
