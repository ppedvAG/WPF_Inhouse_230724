using System.Collections.Generic;

namespace ppedv.CheesyDrive.Model
{
    public class Car : Entity
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int KW { get; set; }
        public string Color { get; set; } = string.Empty;
        public EngineType EngineType { get; set; }
        public int Seats { get; set; }
        public string Plate { get; set; } = string.Empty;

        public virtual ICollection<Rent> Rents { get; set; } = new HashSet<Rent>();
    }

}