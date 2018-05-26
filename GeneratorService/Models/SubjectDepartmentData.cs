using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class SubjectDepartmentData
    {
        public long Id { get; set; }
        public long SubjectId { get; set; }
        public short? DepartmentId { get; set; }
    }
}
