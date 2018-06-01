using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public partial class TeacherDepartment
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public short DepartmentId { get; set; }

        public Department Department { get; set; }
        public Teacher Teacher { get; set; }
    }
}
