using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_InhouseLab.Model;

namespace WPF_InhouseLab.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Wnd_PersonenDialog : Window
    {
        
        public Wnd_PersonenDialog()
        {
            InitializeComponent();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            Person neuePerson = this.DataContext as Person;

            string ausgabe = neuePerson.Vorname + " " + neuePerson.Nachname + " (" + neuePerson.Geschlecht + ")\n" + neuePerson.Geburtsdatum.ToShortDateString() + "\n" + neuePerson.Lieblingsfarbe.ToString();
            if (neuePerson.Verheiratet) ausgabe = ausgabe + "\nIst verheiratet";
            if (MessageBox.Show(ausgabe + "\nAbspeichern?", neuePerson.Vorname + " " + neuePerson.Nachname, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Btn_Abbruch_Click(object sender, RoutedEventArgs e) 
            => this.Close();
    }
}
