using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Building
    {
        public Building()
        {
            Auditorium = new HashSet<Auditorium>();
        }

        public byte Id { get; set; }
        public string FullName { get; set; }
        public byte? Number { get; set; }
        public string Address { get; set; }

        public ICollection<Auditorium> Auditorium { get; set; }
    }
}
