using DomainModel.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Models
{
    public class CriteriaWeight
    {
        public Criteria criteria { get; set; }
        public double Weight { get; set; }
    }
}
