using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class Auditorium
    {
        public Auditorium()
        {
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleRealization = new HashSet<ScheduleRealization>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public short Id { get; set; }
        public short? Number { get; set; }
        public string Name { get; set; }
        public short? DepartmentId { get; set; }
        public short SeatingCapacity { get; set; }
        public byte AuditoriumTypeId { get; set; }
        public byte? BuildingId { get; set; }

        public AuditoriumType AuditoriumType { get; set; }
        public Building Building { get; set; }
        public Department Department { get; set; }
        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleRealization> ScheduleRealization { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
