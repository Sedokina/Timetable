using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleRealization = new HashSet<ScheduleRealization>();
            ScheduleYears = new HashSet<ScheduleYears>();
            TeacherDepartment = new HashSet<TeacherDepartment>();
            TeacherPersonalTime = new HashSet<TeacherPersonalTime>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleRealization> ScheduleRealization { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
        public ICollection<TeacherDepartment> TeacherDepartment { get; set; }
        public ICollection<TeacherPersonalTime> TeacherPersonalTime { get; set; }
    }
}
