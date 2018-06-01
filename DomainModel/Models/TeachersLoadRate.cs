using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Models
{
    public class TeachersLoadRate
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public double Rate { get; set; }
        public int TotalHours { get; set; }
    }
}
