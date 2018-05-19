using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class ScheduleWeeks
    {
        public long Id { get; set; }
        public int ScheduleId { get; set; }
        public byte WeekId { get; set; }

        public Schedule Schedule { get; set; }
        public Week Week { get; set; }
    }
}
