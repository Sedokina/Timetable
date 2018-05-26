using DomainModel.Domain;
using GeneratorService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl
    {
        public List<TeacherPersonalTimeData> GetTeacherPersonalTime(int teacherId)
        {
            return db.TeacherPersonalTime.Where(tpt => tpt.TeacherId == teacherId).Select(tpt => new TeacherPersonalTimeData
            {
                Id = tpt.Id,
                DayOfWeekId = tpt.DayOfWeekId,
                HourId = tpt.HourId,
                TeacherId = tpt.TeacherId
            }).ToList();
        }

        public bool InsertGenTeachers(List<TeacherData> teachers)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Teachers");
                try
                {
                    foreach (TeacherData teach in teachers)
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
                    throw ex;
                }
            }
            return true;
        }

        public int CountTeacherPersonalTime(int teacherId)
        {
            return GetTeacherPersonalTime(teacherId).Count;
        }

        public List<ScheduleData> GetTeacherSchedule(int teacherId)
        {
            return db.Schedule.Where(s => s.TeacherId == teacherId && s.SemesterId == GetCurrentSemester().Id).Select(s => new ScheduleData
            {
                Id = s.Id,
                AuditoriumId = s.AuditoriumId,
                DayOfWeekId = s.DayOfWeekId,
                GroupId = s.DayOfWeekId,
                HourId = s.HourId,
                SemesterId = s.SemesterId,
                SubjectId = s.SubjectId,
                SubjectTypeId = s.SubjectTypeId,
                TeacherId = s.TeacherId
            }).ToList();
        }

    }
}
