using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class GenSubjectClass
    {
        public byte Id { get; set; }
        public byte SubjectClassId { get; set; }
        public double Rate { get; set; }
    }
}
