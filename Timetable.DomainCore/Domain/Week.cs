using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class Week
    {
        public Week()
        {
            RaschasovkaWeeks = new HashSet<RaschasovkaWeeks>();
            ScheduleWeeks = new HashSet<ScheduleWeeks>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public byte Id { get; set; }
        public byte Number { get; set; }
        public bool Znamenatel { get; set; }

        public ICollection<RaschasovkaWeeks> RaschasovkaWeeks { get; set; }
        public ICollection<ScheduleWeeks> ScheduleWeeks { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
