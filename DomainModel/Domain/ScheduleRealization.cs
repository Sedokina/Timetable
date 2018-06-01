using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class ScheduleRealization
    {
        public string Id { get; set; }
        public DateTime? ActualDate { get; set; }
        public TimeSpan? BeginTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? ScheduleId { get; set; }
        public string Description { get; set; }
        public short? ActualAuditoriumId { get; set; }
        public int? ActualTeacherId { get; set; }

        public Auditorium ActualAuditorium { get; set; }
        public Teacher ActualTeacher { get; set; }
        public Schedule Schedule { get; set; }
    }
}
