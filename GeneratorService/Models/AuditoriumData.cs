using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class AuditoriumData
    { 
        public short Id { get; set; }
        public short? Number { get; set; }
        public string Name { get; set; }
        public short? DepartmentId { get; set; }
        public short SeatingCapacity { get; set; }
        public byte AuditoriumTypeId { get; set; }
        public byte? BuildingId { get; set; }
    }
}
