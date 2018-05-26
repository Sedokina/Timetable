using GeneratorService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorService.ModelView
{
    public class ScheduleViewModel
    {
        public AuditoriumData Auditorium { get; set; }
        public DayOfWeekData DayOfWeek { get; set; }
        public GroupData Group { get; set; }
        public HourData Hour { get; set; }
        public SemestersData Semester { get; set; }
        public SubjectData Subject { get; set; }
        public SubjectTypeData SubjectType { get; set; }
        public TeacherData Teacher { get; set; }
    }
}
