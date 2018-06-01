using System;
using System.Collections.Generic;
using System.Text;

namespace Timetable.DomainCore.Models
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
}
