using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class DayOfWeek
    {
        public DayOfWeek()
        {
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
            TeacherPersonalTime = new HashSet<TeacherPersonalTime>();
        }

        public byte Id { get; set; }
        public byte Number { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
        public ICollection<TeacherPersonalTime> TeacherPersonalTime { get; set; }
    }
}
