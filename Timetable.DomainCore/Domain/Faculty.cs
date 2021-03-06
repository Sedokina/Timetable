﻿using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class Faculty
    {
        public Faculty()
        {
            Department = new HashSet<Department>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public ICollection<Department> Department { get; set; }
    }
}
