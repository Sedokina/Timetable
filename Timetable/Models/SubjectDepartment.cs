﻿using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class SubjectDepartment
    {
        public long Id { get; set; }
        public long SubjectId { get; set; }
        public short? DepartmentId { get; set; }

        public Department Department { get; set; }
        public Subject Subject { get; set; }
    }
}
