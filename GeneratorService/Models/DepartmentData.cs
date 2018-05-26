using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class DepartmentData
    {

        public short Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public short? FacultyId { get; set; }
    }
}
