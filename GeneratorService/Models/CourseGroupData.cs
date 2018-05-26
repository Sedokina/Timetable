using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class CourseGroupData
    {
        public long Id { get; set; }
        public byte CourseId { get; set; }
        public long GroupId { get; set; }
    }
}
