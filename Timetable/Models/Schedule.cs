using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleRealization = new HashSet<ScheduleRealization>();
        }

        public int Id { get; set; }
        public byte HourId { get; set; }
        public byte DayOfWeekId { get; set; }
        public long GroupId { get; set; }
        public int TeacherId { get; set; }
        public short AuditoriumId { get; set; }
        public DateTime? LastChange { get; set; }
        public bool? IsFinal { get; set; }
        public long SubjectId { get; set; }
        public byte SubjectTypeId { get; set; }
        public byte SemesterId { get; set; }
        public bool? Week1 { get; set; }
        public bool? Week2 { get; set; }
        public bool? Week3 { get; set; }
        public bool? Week4 { get; set; }
        public bool? Week5 { get; set; }
        public bool? Week6 { get; set; }
        public bool? Week7 { get; set; }
        public bool? Week8 { get; set; }
        public bool? Week9 { get; set; }
        public bool? Week10 { get; set; }
        public bool? Week11 { get; set; }
        public bool? Week12 { get; set; }
        public bool? Week13 { get; set; }
        public bool? Week14 { get; set; }
        public bool? Week15 { get; set; }
        public bool? Week16 { get; set; }
        public bool? Week17 { get; set; }

        public Auditorium Auditorium { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Group Group { get; set; }
        public Hour Hour { get; set; }
        public Semesters Semester { get; set; }
        public Subject Subject { get; set; }
        public SubjectType SubjectType { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<ScheduleRealization> ScheduleRealization { get; set; }
    }
}
