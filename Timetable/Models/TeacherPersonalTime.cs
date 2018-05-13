using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class TeacherPersonalTime
    {
        public long Id { get; set; }
        public int TeacherId { get; set; }
        public byte HourId { get; set; }
        public byte DayOfWeekId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public Hour Hour { get; set; }
        public Teacher Teacher { get; set; }
    }
}
