using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class GenTimeslots
    {
        public long Id { get; set; }
        public byte DayId { get; set; }
        public byte HourId { get; set; }
        public short AuditoriumId { get; set; }
        public double Rate { get; set; }
    }
}
