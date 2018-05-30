using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl
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
        public List<Raschasovka> GetFlawsFromLoad()
        {
            return db.Raschasovka.Where(load => load.Potok != null).ToList();
        }

        public List<Raschasovka> GetGroupsLoadForCurrentSemester()
        {

            return db.Raschasovka.Where(load => load.Semester.Id == GetSemesterByName("Весенний").Id).
                OrderBy(load => load.Course.Number).ToList();
        }
        /*
         * Semester
         * 
         */
        public Semesters GetSemesterByName(string semesterName)
        {
            Semesters semester = db.Semesters.FirstOrDefault(sem => sem.Name == semesterName);
            return new Semesters
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

        public List<Auditorium> GetAuditoriumsForDepartment(short departmentId)
        {
            return db.Auditorium.Where(a => a.DepartmentId == departmentId).Select(a => new Auditorium
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

        public List<Auditorium> GetDepartmentAuditoriumsForSubjectType(short departmentId, byte subjectTypeId)
        {
            var auditoriumsSubjects = db.AuditoriumSubjectTypes.Where(ast => ast.SubjectTypeId == subjectTypeId).ToList();
            return db.Auditorium.Where(a => a.DepartmentId == departmentId &&
                auditoriumsSubjects.Exists(ast => ast.AuditoriumTypeId == a.AuditoriumTypeId)).Select(a => new Auditorium
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
        
        public List<Faculty> GetFacultiesView()
        {
            return db.Faculty.Select(f=>new Faculty {
                Id = f.Id,
                FullName = f.FullName,
                Name = f.Name
            }).ToList();
        }

        public byte GetCurrentWeek()
        {
            return db.Week.FirstOrDefault().Id;
        }
    }
}
