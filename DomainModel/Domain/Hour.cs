using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class Hour
    {
        public Hour()
        {
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
            TeacherPersonalTime = new HashSet<TeacherPersonalTime>();
        }

        public byte Id { get; set; }
        public TimeSpan Begin { get; set; }
        public TimeSpan End { get; set; }
        public byte Number { get; set; }

        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
        public ICollection<TeacherPersonalTime> TeacherPersonalTime { get; set; }
    }
}
