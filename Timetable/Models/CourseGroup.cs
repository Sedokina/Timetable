﻿using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class CourseGroup
    {
        public long Id { get; set; }
        public byte CourseId { get; set; }
        public long GroupId { get; set; }

        public Course Course { get; set; }
        public Group Group { get; set; }
    }
}
