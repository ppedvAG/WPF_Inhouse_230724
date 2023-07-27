using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.CheesyDrive.UI.WPF.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private IRepository repo;
        private Car selectedCar;

        public CarViewModel()
        {
            string conString = "Server=(localdb)\\mssqllocaldb;Database=CheesyDrive_Test;Trusted_Connection=true";
            repo = new Data.EfCore.EfRepository(conString);

            CarList = new List<Car>(repo.Query<Car>());
            SaveCommand = new SaveCommand(repo);
        }
        public ICommand SaveCommand { get; set; }


        public string PS
        {
            get
            {
                if (SelectedCar == null)
                    return "nix";

                const double kilowattToPferdestarke = 1.35962;

                double pferdestarke = SelectedCar.KW * kilowattToPferdestarke;
                return pferdestarke.ToString("F2"); // Formatieren auf 2 Dezimalstellen
            }
        }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PS)));
            }
        }
        public List<Car> CarList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
