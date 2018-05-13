using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Criteria
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
    }
}
