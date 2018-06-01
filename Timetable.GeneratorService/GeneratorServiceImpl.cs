using Timetable.DomainCore.Domain;
using Timetable.DomainCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timetable.GeneratorService
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

            return db.Raschasovka.Where(load => load.Semester.Id == GetSemesterByName("Весенний").Id)
                .Include(l=>l.RaschasovkaWeeks)
                .OrderBy(load => load.Course.Number).ToList();
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

        //public List<TeachersLoadWeight> GetTeachersLoadRate()
        //{
        //    int i = 0;
        //    var GenList = db.Raschasovka.GroupBy(r => new { r.TeacherId, r.TotalHoursForSemestr })
        //        .Select(r => new TeachersLoadRate { Id = 0, Rate = r.Count() + r.Key.TotalHoursForSemestr, TeacherId = r.Key.TeacherId, TotalHours = r.Key.TotalHoursForSemestr }).ToList();
        //    foreach (var item in GenList)
        //        item.Id = ++i;
        //    InsertGenTeachers(GenList.Select(l=>new RateData {Id = l.Id, Rate = l.Rate }).ToList());
        //    return GenList.Join(GetTeachersWeight(), l => l.Id, w => w.Id, (l, w) => new TeachersLoadWeight { TeacherId = l.TeacherId, TotalHours = l.TotalHours, Weight = w.Weight }).ToList();
        //}

        public List<RateData> GetTeachersLoadRate()
        {
            //return db.Raschasovka.GroupBy(r => new { r.TeacherId, r.TotalHoursForSemestr })
            //    .Select(r => new RateData { Rate = r.Count() + r.Key.TotalHoursForSemestr, Id = r.Key.TeacherId }).ToList();
            return db.Raschasovka.GroupBy(r => r.TeacherId)
                .Select(r => new RateData { Rate = r.Count(), Id = r.Key }).ToList();
        }
    }
}
