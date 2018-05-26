using DomainModel.Domain;
using GeneratorService;
using GeneratorService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl : IGeneratorService
    {
        ScheduleKSTUContext db;
        public GeneratorServiceImpl()
        {
            db = new ScheduleKSTUContext();
        }

        /*
         * Load
         *  
         */
        public List<LoadData> GetFlawsFromLoad()
        {
            return db.Raschasovka.Where(load => load.Potok != null).Select(l => new LoadData
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

        public List<LoadData> GetGroupsLoadForCurrentSemester()
        {

            return db.Raschasovka.Where(load => load.Semester.Id == GetSemesterByName("Весенний").Id).
                OrderBy(load => load.Course.Number).Select(l => new LoadData
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
        /*
         * Semester
         * 
         */
        public SemestersData GetSemesterByName(string semesterName)
        {
            Semesters semester = db.Semesters.FirstOrDefault(sem => sem.Name == semesterName);
            return new SemestersData
            {
                Id = semester.Id,
                Name = semester.Name
            };
        }

        public Semesters GetCurrentSemester()
        {
            string semesterName;
            if (DateTime.Now.Month / 6 >= 1)
                semesterName = "Осенний";
            else
                semesterName = "Весенний";
            return db.Semesters.FirstOrDefault(s => s.Name == semesterName);
        }

        /*
         * Auditoriums
         * 
         */

        public List<AuditoriumData> GetAuditoriumsForDepartment(short departmentId)
        {
            return db.Auditorium.Where(a => a.DepartmentId == departmentId).Select(a => new AuditoriumData
            {
                Id = a.Id,
                AuditoriumTypeId = a.AuditoriumTypeId,
                BuildingId = a.BuildingId,
                DepartmentId = a.DepartmentId,
                Name = a.Name,
                Number = a.Number,
                SeatingCapacity = a.SeatingCapacity
            }).ToList();
        }

        public List<AuditoriumData> GetDepartmentAuditoriumsForSubjectType(short departmentId, byte subjectTypeId)
        {
            var auditoriumsSubjects = db.AuditoriumSubjectTypes.Where(ast => ast.SubjectTypeId == subjectTypeId).ToList();
            return db.Auditorium.Where(a => a.DepartmentId == departmentId &&
                auditoriumsSubjects.Exists(ast => ast.AuditoriumTypeId == a.AuditoriumTypeId)).Select(a => new AuditoriumData
                {
                    Id = a.Id,
                    AuditoriumTypeId = a.AuditoriumTypeId,
                    BuildingId = a.BuildingId,
                    DepartmentId = a.DepartmentId,
                    Name = a.Name,
                    Number = a.Number,
                    SeatingCapacity = a.SeatingCapacity
                }).ToList();
        }

        /*
         * 
         * Faculties
         * 
         */
        
        public List<FacultyData> GetFacultiesView()
        {
            return db.Faculty.Select(f=>new FacultyData {
                Id = f.Id,
                FullName = f.FullName,
                Name = f.Name
            }).ToList();
        }
    }
}
