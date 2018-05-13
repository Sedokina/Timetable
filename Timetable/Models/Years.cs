using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Years
    {
        public Years()
        {
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
