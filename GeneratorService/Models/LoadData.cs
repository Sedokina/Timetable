using System;
using System.Collections.Generic;

namespace GeneratorService.Models
{
    public class LoadData
    {
        public int Id { get; set; }
        public int? Flaw { get; set; }
        public byte CourseId { get; set; }
        public int TeacherId { get; set; }
        public short TotalHoursForSemestr { get; set; }
        public short? AuditoriumId { get; set; }
        public long GroupId { get; set; }
        public short DepartmentId { get; set; }
        public byte SemesterId { get; set; }
        public long SubjectId { get; set; }
        public byte SubjectTypeId { get; set; }
        public byte NumberOfStudents { get; set; }
        public byte SubjectClassId { get; set; }
    }
}
