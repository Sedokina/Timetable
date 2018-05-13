using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class RaschasovkaWeeks
    {
        public long Id { get; set; }
        public int RaschasovkaId { get; set; }
        public byte WeekId { get; set; }
        public byte HoursForWeek { get; set; }

        public Raschasovka Raschasovka { get; set; }
        public Week Week { get; set; }
    }
}
