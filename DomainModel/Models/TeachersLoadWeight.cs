﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Models
{
    public class TeachersLoadWeight
    {
        public int TeacherId { get; set; }
        public int TotalHours { get; set; }
        public double Weight { get; set; }
    }
}
