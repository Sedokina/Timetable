using System;
using System.Collections.Generic;
using System.Text;
using DataModel.Models;
namespace GeneratorLogic.Models
{
    public class TimeslotsCriteriaWeight
    {
        public byte HourId { get; set; }
        public byte DayId { get; set; }
        public short AuditoriumId { get; set; }
        public double AllCriteriaWeight { get; set; }
        public List<CriteriaWeight> criteriaWeight { get; set; }

        public TimeslotsCriteriaWeight()
        {
            criteriaWeight = new List<CriteriaWeight>();
        }
    }

    public class CriteriaWeight
    {
        public Criteria criteria { get; set; }
        public double Weight { get; set; }
    }
}
