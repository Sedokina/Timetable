using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class ScheduleYears
    {
        public int Id { get; set; }
        public byte HourId { get; set; }
        public byte DayOfWeekId { get; set; }
        public long GroupId { get; set; }
        public int TeacherId { get; set; }
        public short AuditoriumId { get; set; }
        public byte WeekId { get; set; }
        public DateTime? LastChange { get; set; }
        public bool? IsFinal { get; set; }
        public long SubjectId { get; set; }
        public byte SubjectTypeId { get; set; }
        public byte SemesterId { get; set; }
        public byte YearId { get; set; }

        public Auditorium Auditorium { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Group Group { get; set; }
        public Hour Hour { get; set; }
        public Semesters Semester { get; set; }
        public Subject Subject { get; set; }
        public SubjectType SubjectType { get; set; }
        public Teacher Teacher { get; set; }
        public Week Week { get; set; }
        public Years Year { get; set; }
    }
}
