using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public partial class BuildingData
    {
        public byte Id { get; set; }
        public string FullName { get; set; }
        public byte? Number { get; set; }
        public string Address { get; set; }
    }
}
