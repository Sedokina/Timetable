using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class GenTeachers
    {
        public long Id { get; set; }
        public int TeacherId { get; set; }
        public double? Rate { get; set; }

        public Teacher Teacher { get; set; }
    }
}
