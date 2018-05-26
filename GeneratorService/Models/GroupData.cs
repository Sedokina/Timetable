using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class GroupData
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public byte? NumberOfStudents { get; set; }
        public short DepartmentId { get; set; }
    }
}
