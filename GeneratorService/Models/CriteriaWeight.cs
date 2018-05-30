using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.Domain;

namespace GeneratorService.Models
{
    public class CriteriaWeight
    {
        public Criteria criteria { get; set; }
        public double Weight { get; set; }
    }
}
