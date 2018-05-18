﻿using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseGroup = new HashSet<CourseGroup>();
            Raschasovka = new HashSet<Raschasovka>();
            RaschasovkaYears = new HashSet<RaschasovkaYears>();
        }

        public byte Id { get; set; }
        public byte Number { get; set; }

        public ICollection<CourseGroup> CourseGroup { get; set; }
        public ICollection<Raschasovka> Raschasovka { get; set; }
        public ICollection<RaschasovkaYears> RaschasovkaYears { get; set; }
    }
}