using DomainModel.Domain;
using GeneratorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl
    {

        public List<CourseData> GetCourses()
        {
            return db.Course.Select(c => new CourseData
            {
                Id = c.Id,
                Number = c.Number
            }).ToList();
        }

        public List<TeacherData> GetTeachers()
        {
            return db.Teacher.Select(t => new TeacherData
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName
            }).ToList();
        }

        public List<AuditoriumData> GetAuditoriums()
        {
            return db.Auditorium.Select(a => new AuditoriumData
            {
                Id = a.Id,
                AuditoriumTypeId = a.AuditoriumTypeId,
                BuildingId = a.BuildingId,
                Number = a.Number,
                Name = a.Name,
                DepartmentId = a.DepartmentId,
                SeatingCapacity = a.SeatingCapacity
            }).ToList();
        }

        public List<GroupData> GetGroups()
        {
            return db.Group.Select(g => new GroupData
            {
                Id = g.Id,
                Name = g.Name,
                DepartmentId = g.DepartmentId,
                NumberOfStudents = g.NumberOfStudents
            }).ToList();
        }

        public List<DepartmentData> GetDepartments()
        {
            return db.Department.Select(d => new DepartmentData
            {
                Id = d.Id,
                Name = d.Name,
                FullName = d.FullName,
                FacultyId = d.FacultyId
            }).ToList();
        }

        public List<SemestersData> GetSemesters()
        {
            return db.Semesters.Select(s => new SemestersData
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public List<SubjectData> GetSubjects()
        {
            return db.Subject.Select(s => new SubjectData
            {
                Id = s.Id,
                Name = s.Name,
                FullName = s.FullName
            }).ToList();
        }

        public List<SubjectTypeData> GetSubjectTypes()
        {
            return db.SubjectType.Select(s => new SubjectTypeData
            {
                Id = s.Id,
                Name = s.Name,
                FullName = s.FullName
            }).ToList();
        }

        public List<WeekData> GetWeeks()
        {
            return db.Week.Select(w => new WeekData
            {
                Id = w.Id,
                Number = w.Number,
                Denomenator = w.Znamenatel
            }).ToList();
        }

        public List<FacultyData> GetFaculties()
        {
            return db.Faculty.Select(f => new FacultyData
            {
                Id = f.Id,
                Name = f.Name,
                FullName = f.FullName
            }).ToList();
        }

        public List<LoadData> GetLoad()
        {
            return db.Raschasovka.Select(l => new LoadData
            {
                Id = l.Id,
                AuditoriumId = l.AuditoriumId,
                CourseId = l.CourseId,
                DepartmentId = l.DepartmentId,
                GroupId = l.GroupId,
                NumberOfStudents = l.NumberOfStudents,
                Flaw = l.Potok,
                SemesterId = l.SemesterId,
                SubjectClassId = l.SubjectClassId,
                SubjectId = l.SubjectId,
                SubjectTypeId = l.SubjectTypeId,
                TeacherId = l.TeacherId,
                TotalHoursForSemestr = l.TotalHoursForSemestr
            }).ToList();
        }

        public List<HourData> GetHours()
        {
            return db.Hour.Select(h => new HourData
            {
                Id = h.Id,
                Begin = h.Begin,
                End = h.End,
                Number = h.Number
            }).ToList();
        }

        public List<DayOfWeekData> GetDaysOfWeek()
        {
            return db.DayOfWeek.Select(d => new DayOfWeekData
            {
                Id = d.Id,
                Number = d.Number,
                Name = d.Name,
                FullName = d.FullName
            }).ToList();
        }

        public List<CriteriaData> GetCriteria()
        {
            return db.Criteria.Select(c => new CriteriaData
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Rate = c.Rate
            }).ToList();
        }
    }
}
