using DomainModel.Domain;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorServiceServer
{
    public partial class GeneratorServiceImpl
    {
        public List<TeacherPersonalTime> GetTeacherPersonalTime(int teacherId)
        {
            return db.TeacherPersonalTime.Where(tpt => tpt.TeacherId == teacherId).Select(tpt => new TeacherPersonalTime
            {
                Id = tpt.Id,
                DayOfWeekId = tpt.DayOfWeekId,
                HourId = tpt.HourId,
                TeacherId = tpt.TeacherId
            }).ToList();
        }

        public bool InsertGenTeachers(List<Teacher> teachers)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Teachers");
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
                    throw ex;
                }
            }
            return true;
        }

        public int CountTeacherPersonalTime(int teacherId)
        {
            return GetTeacherPersonalTime(teacherId).Count;
        }

        public List<Schedule> GetTeacherSchedule(Raschasovka load)
        {
            bool weeksContains = false;
            var scheduleList = db.Schedule
                .Include(s => s.ScheduleWeeks)
                .Where(s => s.TeacherId == load.TeacherId && s.SemesterId == GetCurrentSemester().Id);
            List<Schedule> teachersFree = new List<Schedule>();
            foreach(var s in scheduleList)
            {
                weeksContains = load.RaschasovkaWeeks.Select(w => new Week { Id = w.WeekId }).Except(s.ScheduleWeeks.Select(sw => new Week
                {
                    Id = sw.WeekId,
                })).Any();
                if (weeksContains == true)
                    teachersFree.Add(s);
            }
            scheduleList.Except(teachersFree);
            return scheduleList.ToList();
        }

    }
}
