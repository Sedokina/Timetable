using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class Department
    {
        public Department()
        {
            Auditorium = new HashSet<Auditorium>();
            Group = new HashSet<Group>();
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            SubjectDepartment = new HashSet<SubjectDepartment>();
            TeacherDepartment = new HashSet<TeacherDepartment>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public short? FacultyId { get; set; }

        public Faculty Faculty { get; set; }
        public ICollection<Auditorium> Auditorium { get; set; }
        public ICollection<Group> Group { get; set; }
        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<SubjectDepartment> SubjectDepartment { get; set; }
        public ICollection<TeacherDepartment> TeacherDepartment { get; set; }
    }
}
