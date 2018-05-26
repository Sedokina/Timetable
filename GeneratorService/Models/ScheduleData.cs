using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class ScheduleData
    {
        public int Id { get; set; }
        public byte HourId { get; set; }
        public byte DayOfWeekId { get; set; }
        public long GroupId { get; set; }
        public int TeacherId { get; set; }
        public short AuditoriumId { get; set; }
        public long SubjectId { get; set; }
        public byte SubjectTypeId { get; set; }
        public byte SemesterId { get; set; }
    }
}
