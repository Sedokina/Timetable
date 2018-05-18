﻿using System;
using System.Collections.Generic;
using System.Text;
using DataModel.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GeneratorLogic.Models;
namespace GeneratorLogic
{
    public class GeneratorServices
    {
        ScheduleKSTUContext db;
        public GeneratorServices()
        {
            db = new ScheduleKSTUContext();
        }

        #region All list get methods
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

        public List<Schedule> GetFacultySchedule(int facultyId)
        {
            return db.Schedule.Include(s => s.Auditorium)
                .Include(s => s.DayOfWeek)
                .Include(s => s.Group)
                .Include(s => s.Hour)
                .Include(s => s.Semester)
                .Include(s => s.Subject)
                .Include(s => s.SubjectType)
                .Include(s => s.Teacher).Where(s => s.Group.Department.FacultyId == facultyId).ToList();
        }

        public List<Raschasovka> GetLoad()
        {
            return db.Raschasovka.Include(load => load.Semester).
                Include(load => load.Course).
                Include(load => load.Teacher).ToList();
        }

        public List<Hour> GetHours()
        {
            return db.Hour.ToList();
        }

        public List<DataModel.Models.DayOfWeek> GetDaysOfWeek()
        {
            return db.DayOfWeek.ToList();
        }

        public List<Criteria> GetCriteria()
        {
            return db.Criteria.ToList();
        }
        #endregion

        public List<Raschasovka> GetFlawsFromLoad()
        {
            return db.Raschasovka.Where(load => load.Potok != null).ToList();
        }

        public Semesters GetSemesterByName(string semesterName)
        {
            return db.Semesters.FirstOrDefault(semester => semester.Name == semesterName);
        }

        public List<TeacherPersonalTime> GetTeacherPersonalTime(int teacherId)
        {
            return db.TeacherPersonalTime.Where(tpt => tpt.TeacherId == teacherId)
                .Include(tpt => tpt.Hour)
                .Include(tpt => tpt.DayOfWeek).ToList();
        }

        public bool InsertGenTeachers(List<Teacher> teachers)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Teachers");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT(Gen_Teachers, RESEED, 1)");
                try
                {
                    foreach (Teacher teach in teachers)
                    {
                        db.GenTeachers.Add(new GenTeachers
                        {
                            TeacherId = teach.Id,
                            Rate = CountTeacherPersonalTime(teach.Id) + 1
                        });
                    }
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
            return true;
        }

        public int CountTeacherPersonalTime(int teacherId)
        {
            return GetTeacherPersonalTime(teacherId).Count;
        }

        public List<TableWeight> GetTeachersWeight()
        {
            return db.Set<TableWeight>().FromSql("dbo.GetTeachersWeight").ToList();
        }

        public List<TableWeight> GetSubjectClassWeight()
        {
            return db.Set<TableWeight>().FromSql("dbo.GetSubjectClassWeight").ToList();
        }

        public List<Schedule> GetTeacherSchedule(int teacherId)
        {
            return db.Schedule.Where(s => s.TeacherId == teacherId && s.SemesterId == GetCurrentSemester().Id)
                .Include(s=>s.Hour).ToList();
        }

        public List<Schedule> GetGroupSchedule(long groupId)
        {
            return db.Schedule.Where(s => s.GroupId == groupId && s.SemesterId == GetCurrentSemester().Id).ToList();
        }

        public List<Schedule> GetAuditoriumSchedule(short auditoriumId)
        {
            return db.Schedule.Where(s => s.AuditoriumId == auditoriumId && s.SemesterId == GetCurrentSemester().Id).ToList();
        }

        public List<Auditorium> GetAuditoriumsForDepartment(short departmentId)
        {
            return db.Auditorium.Where(a => a.DepartmentId == departmentId).ToList();
        }

        public List<Auditorium> GetDepartmentAuditoriumsForSubjectType(short departmentId, byte subjectTypeId)
        {
            var auditoriumsSubjects = db.AuditoriumSubjectTypes.Where(ast => ast.SubjectTypeId == subjectTypeId).ToList();
            return db.Auditorium.Where(a=>a.DepartmentId == departmentId && 
                auditoriumsSubjects.Exists(ast=>ast.AuditoriumTypeId == a.AuditoriumTypeId)).ToList();
        }

        public Semesters GetCurrentSemester()
        {
            string semesterName;
            if (DateTime.Now.Month / 6 >= 1)
                semesterName = "Осенний";
            else
                semesterName = "Весенний";
            return db.Semesters.FirstOrDefault(s=>s.Name == semesterName);
        }

        public bool InsertGenTimeslots(List<CriteriaRate> criteriaRate)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Timeslots");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT(Gen_Timeslots, RESEED, 1)");
                try
                {
                    foreach (CriteriaRate cr in criteriaRate)
                    {
                        //db.GenTeachers.Add(new GenTeachers
                        //{
                        //    TeacherId = teach.Id,
                        //    Rate = CountTeacherPersonalTime(teach.Id) + 1
                        //});
                    }
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
            return true;
        }

    }
}
