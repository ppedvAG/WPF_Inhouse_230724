using Microsoft.Extensions.DependencyInjection;
using ppedv.CheesyDrive.Data.EfCore;
using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.UI.WPF.ViewModels;
using System;
using System.Windows;

namespace ppedv.CheesyDrive.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            string conString = "Server=(localdb)\\mssqllocaldb;Database=CheesyDrive_Test;Trusted_Connection=true";

            services.AddSingleton<IRepository>(new EfRepository(conString));

            services.AddTransient<CarViewModel>();


            return services.BuildServiceProvider();
        }
    }
}
