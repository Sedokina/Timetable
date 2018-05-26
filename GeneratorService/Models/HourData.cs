using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class HourData
    {
        public byte Id { get; set; }
        public TimeSpan Begin { get; set; }
        public TimeSpan End { get; set; }
        public byte Number { get; set; }
    }
}
