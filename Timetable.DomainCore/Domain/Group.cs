﻿using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class Group
    {
        public Group()
        {
            CourseGroup = new HashSet<CourseGroup>();
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
            Schedule = new HashSet<Schedule>();
            ScheduleYears = new HashSet<ScheduleYears>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public byte? NumberOfStudents { get; set; }
        public short DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<CourseGroup> CourseGroup { get; set; }
        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleYears> ScheduleYears { get; set; }
    }
}
