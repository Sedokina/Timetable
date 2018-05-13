using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Semesters
    {
        public Semesters()
        {
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
