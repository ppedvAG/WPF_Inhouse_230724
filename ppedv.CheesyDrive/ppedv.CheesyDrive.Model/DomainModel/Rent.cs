using System;

namespace ppedv.CheesyDrive.Model.DomainModel
{
    public class Rent : Entity
    {
        public DateTime BookDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }

}