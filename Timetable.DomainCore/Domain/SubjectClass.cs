using System;
using System.Collections.Generic;

namespace Timetable.DomainCore.Domain
{
    public partial class SubjectClass
    {
        public SubjectClass()
        {
            Raschasovka = new HashSet<Raschasovka>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<Raschasovka> Raschasovka { get; set; }
    }
}
