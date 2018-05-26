using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class Subject
    {
        public Subject()
        {
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
            SubjectDepartment = new HashSet<SubjectDepartment>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
        public ICollection<SubjectDepartment> SubjectDepartment { get; set; }
    }
}
