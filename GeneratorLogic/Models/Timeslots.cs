using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorLogic.Models
{
    public class Timeslots
    {
        public byte HourId { get; set; }
        public byte DayId { get; set; }
        public short AuditoriumId { get; set; }
    }

    //public class TimeslotsEqualityComparer : IEqualityComparer<Timeslots>
    //{
    //    public bool Equals(Timeslots x, Timeslots y)
    //    {
    //        return (x.AuditoriumId == y.AuditoriumId && x.DayId == y.DayId && x.HourId == y.HourId);
    //    }

    //    public int GetHashCode(Timeslots obj)
    //    {
    //        return obj.HourId.GetHashCode() ^ obj.DayId.GetHashCode() ^ obj.AuditoriumId.GetHashCode();
    //    }
    //}
}
