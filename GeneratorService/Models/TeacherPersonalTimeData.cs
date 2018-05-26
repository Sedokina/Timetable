using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class TeacherPersonalTimeData
    {
        public long Id { get; set; }
        public int TeacherId { get; set; }
        public byte HourId { get; set; }
        public byte DayOfWeekId { get; set; }
    }
}
