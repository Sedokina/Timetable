using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class SubjectType
    {
        public SubjectType()
        {
            AuditoriumSubjectTypes = new HashSet<AuditoriumSubjectTypes>();
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public ICollection<AuditoriumSubjectTypes> AuditoriumSubjectTypes { get; set; }
        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
