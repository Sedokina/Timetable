using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Models
{
    public class TimeslotsWeight
    {
        public int Id { get; set; }
        public byte HourId { get; set; }
        public byte DayId { get; set; }
        public short AuditoriumId { get; set; }
        public double Weight { get; set; }
    }
}
