using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class AuditoriumType
    {
        public AuditoriumType()
        {
            Auditorium = new HashSet<Auditorium>();
            AuditoriumSubjectTypes = new HashSet<AuditoriumSubjectTypes>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        public ICollection<Auditorium> Auditorium { get; set; }
        public ICollection<AuditoriumSubjectTypes> AuditoriumSubjectTypes { get; set; }
    }
}
