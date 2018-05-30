using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Models
{
    public class Timeslots
    {
        public byte HourId { get; set; }
        public byte DayId { get; set; }
        public short AuditoriumId { get; set; }
    }
}
