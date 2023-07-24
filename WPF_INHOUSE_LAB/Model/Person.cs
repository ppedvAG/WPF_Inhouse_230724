using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace WPF_InhouseLab.Model
{
    public enum Gender { Männlich, Weiblich, Divers }

    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string vorname;
        public string Vorname { get => vorname; set { vorname = value; OnPropertyChanged(); } }

        private string nachname;
        public string Nachname { get => nachname; set { nachname = value; OnPropertyChanged(); } }

        private DateTime geburtsdatum;
        public DateTime Geburtsdatum { get => geburtsdatum; set { geburtsdatum = value; OnPropertyChanged(); } }

        private bool verheiratet;
        public bool Verheiratet { get => verheiratet; set { verheiratet = value; OnPropertyChanged(); } }

        private Color lieblingsfarbe;
        public Color Lieblingsfarbe { get => lieblingsfarbe; set { lieblingsfarbe = value; OnPropertyChanged(); } }

        private Gender geschlecht;
        public Gender Geschlecht { get => geschlecht; set { geschlecht = value; OnPropertyChanged(); } }

        public Person()
        {
            this.Geburtsdatum = DateTime.Now;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
