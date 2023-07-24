using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_InhouseLab.Model;

namespace WPF_InhouseLab.Views
{
    /// <summary>
    /// Interaction logic for DbAnsicht.xaml
    /// </summary>
    public partial class Wnd_DbAnsicht : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Person> Personenliste { get; set; }

        private DbController dbController;


        public Wnd_DbAnsicht()
        {
            InitializeComponent();

            dbController = new DbController();

            Personenliste = dbController.GetPeople();

            this.DataContext = this;
        }

        private void Mei_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Loeschen_Click(object sender, RoutedEventArgs e)
        {
            Person person = Dgd_Personen.SelectedItem as Person;

            if (MessageBox.Show($"Soll diese Person wirklich gelöscht werden?", $"Person löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {                
                dbController.RemovePerson(person);

                Personenliste = dbController.GetPeople();
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Personenliste)));
            }
        }

        private void Btn_Neu_Click(object sender, RoutedEventArgs e)
        {
            Wnd_PersonenDialog dialog = new Wnd_PersonenDialog();

            if (dialog.ShowDialog() == true)
            {
                dbController.AddPerson(dialog.DataContext as Person);

                Personenliste = dbController.GetPeople();

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Personenliste)));
            }
        }
    }
}
