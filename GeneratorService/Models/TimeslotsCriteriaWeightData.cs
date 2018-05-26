using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorService.Models
{
    public class TimeslotsCriteriaWeightData
    {
        public byte HourId { get; set; }
        public byte DayId { get; set; }
        public short AuditoriumId { get; set; }
        public double AllCriteriaWeight { get; set; }
        public List<CriteriaWeightData> criteriaWeight { get; set; }

        public TimeslotsCriteriaWeightData()
        {
            criteriaWeight = new List<CriteriaWeightData>();
        }
    }
}
