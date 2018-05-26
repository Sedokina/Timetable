using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class RaschasovkaYears
    {
        public int Id { get; set; }
        public int Potok { get; set; }
        public byte CourseId { get; set; }
        public int TeacherId { get; set; }
        public short TotalHoursForSemestr { get; set; }
        public short? AuditoriumId { get; set; }
        public long GroupId { get; set; }
        public short DepartmentId { get; set; }
        public byte SemesterId { get; set; }
        public long SubjectId { get; set; }
        public byte SubjectTypeId { get; set; }
        public byte YearId { get; set; }

        public Auditorium Auditorium { get; set; }
        public Course Course { get; set; }
        public Department Department { get; set; }
        public Group Group { get; set; }
        public Semesters Semester { get; set; }
        public Subject Subject { get; set; }
        public SubjectType SubjectType { get; set; }
        public Teacher Teacher { get; set; }
        public Years Year { get; set; }
    }
}
