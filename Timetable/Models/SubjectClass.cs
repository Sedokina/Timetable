﻿using System;
using System.Collections.Generic;

namespace DataModel.Models
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
