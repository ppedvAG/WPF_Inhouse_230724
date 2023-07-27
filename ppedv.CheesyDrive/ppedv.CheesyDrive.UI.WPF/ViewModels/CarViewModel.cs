using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.CheesyDrive.Model.Contracts;
using ppedv.CheesyDrive.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.CheesyDrive.UI.WPF.ViewModels
{
    public class CarViewModel : ObservableObject
    {
        private IRepository _repo;
        private Car selectedCar;

        public CarViewModel(IRepository repo)
        {
            _repo = repo;
            CarList = new ObservableCollection<Car>(repo.Query<Car>());
            SaveCommand = new SaveCommand(repo);
            NewCommand = new RelayCommand(UserWantsAddNewCar);
        }

        private void UserWantsAddNewCar()
        {
            var newCar = new Car() { Model = "NEU" };
            _repo.Add(newCar);
            CarList.Add(newCar);
            SelectedCar = newCar;
        }


        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }


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
                //OnPropertyChanged(); --> "SelectedCar"
                OnPropertyChanged("");
                OnPropertyChanged(nameof(PS));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PS)));
            }
        }
        public ObservableCollection<Car> CarList { get; set; }

    }
}
