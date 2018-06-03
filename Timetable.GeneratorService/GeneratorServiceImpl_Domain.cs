using Timetable.DomainCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timetable.DAL;

namespace Timetable.GeneratorService
{
    public partial class GeneratorServiceImpl
    {

        public List<Course> GetCourses()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Course.ToList();
            }
        }

        public List<Teacher> GetTeachers()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Teacher.ToList();
            }
        }

        public List<Auditorium> GetAuditoriums()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Auditorium.ToList();
            }
        }

        public List<Group> GetGroups()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Group.ToList();
            }
        }

        public List<Department> GetDepartments()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Department.ToList();
            }
        }

        public List<Semesters> GetSemesters()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Semesters.ToList();
            }
        }

        public List<Subject> GetSubjects()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Subject.ToList();
            }
        }

        public List<SubjectType> GetSubjectTypes()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.SubjectType.ToList();
            }
        }

        public List<Week> GetWeeks()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Week.ToList();
            }
        }

        public List<Faculty> GetFaculties()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Faculty.ToList();
            }
        }

        public List<Raschasovka> GetLoad()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Raschasovka.ToList();
            }
        }

        public List<Hour> GetHours()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Hour.ToList();
            }
        }

        public List<Timetable.DomainCore.Domain.DayOfWeek> GetDaysOfWeek()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.DayOfWeek.ToList();
            }
        }

        public List<Criteria> GetCriteria()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Criteria.ToList();
            }
        }

        public Faculty GetFacultie(int id)
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Faculty.FirstOrDefault(f => f.Id == id);
            }
        }
        public Week GetWeekNumber(byte id)
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Week.FirstOrDefault(w => w.Id == id);
            }
        }
    }
}
