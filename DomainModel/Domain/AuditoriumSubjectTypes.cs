﻿using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class AuditoriumSubjectTypes
    {
        public byte Id { get; set; }
        public byte AuditoriumTypeId { get; set; }
        public byte SubjectTypeId { get; set; }

        public AuditoriumType AuditoriumType { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}