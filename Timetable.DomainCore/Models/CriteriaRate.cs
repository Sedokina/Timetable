﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Timetable.DomainCore.Models
{
    public class CriteriaRate
    {
        public Timeslots timeslots { get; set; }
        public float Rate { get; set; }
    }
}
