using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class LoadWeeksData
    {
        public long Id { get; set; }
        public int RaschasovkaId { get; set; }
        public byte WeekId { get; set; }
        public byte HoursForWeek { get; set; }
    }
}
