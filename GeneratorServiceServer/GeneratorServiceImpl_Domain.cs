using DomainModel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl
    {

        public List<Course> GetCourses()
        {
            return db.Course.ToList();
        }

        public List<Teacher> GetTeachers()
        {
            return db.Teacher.ToList();
        }

        public List<Auditorium> GetAuditoriums()
        {
            return db.Auditorium.ToList();
        }

        public List<Group> GetGroups()
        {
            return db.Group.ToList();
        }

        public List<Department> GetDepartments()
        {
            return db.Department.ToList();
        }

        public List<Semesters> GetSemesters()
        {
            return db.Semesters.ToList();
        }

        public List<Subject> GetSubjects()
        {
            return db.Subject.ToList();
        }

        public List<SubjectType> GetSubjectTypes()
        {
            return db.SubjectType.ToList();
        }

        public List<Week> GetWeeks()
        {
            return db.Week.ToList();
        }

        public List<Faculty> GetFaculties()
        {
            return db.Faculty.ToList();
        }

        public List<Raschasovka> GetLoad()
        {
            return db.Raschasovka.ToList();
        }

        public List<Hour> GetHours()
        {
            return db.Hour.ToList();
        }

        public List<DomainModel.Domain.DayOfWeek> GetDaysOfWeek()
        {
            return db.DayOfWeek.ToList();
        }

        public List<Criteria> GetCriteria()
        {
            return db.Criteria.ToList();
        }
    }
}
