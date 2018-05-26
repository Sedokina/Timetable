using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class ScheduleWeeksData
    {
        public long Id { get; set; }
        public int ScheduleId { get; set; }
        public byte WeekId { get; set; }
    }
}
