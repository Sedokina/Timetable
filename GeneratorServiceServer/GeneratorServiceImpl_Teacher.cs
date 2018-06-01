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

        public bool InsertGenTeachers(List<RateData> teachers)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Teachers");
                try
                {
                    foreach (RateData teach in teachers)
                    {
                        db.GenTeachers.Add(new GenTeachers
                        {
                            TeacherId = teach.Id,
                            Rate = teach.Rate + 1
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
            List<Schedule> scheduleList = db.Schedule
                .Include(s => s.ScheduleWeeks)
                .Where(s => s.TeacherId == load.TeacherId && s.SemesterId == GetCurrentSemester().Id).ToList();
            List<Schedule> teachersFree = new List<Schedule>();
            foreach(var s in scheduleList)
            {
                //weeksContains = !load.RaschasovkaWeeks.Where(rw => rw.HoursForWeek != 0).Select(w => new Week { Id = w.WeekId }).Except(s.ScheduleWeeks.Select(sw => new Week
                //{
                //    Id = sw.WeekId,
                //})).Any();
                var set = new HashSet<int>(load.RaschasovkaWeeks.Where(rw => rw.HoursForWeek != 0).Select(w => (int)w.WeekId));
                var set2 = s.ScheduleWeeks.Select(sw => (int)sw.WeekId);
                var equals = set.SetEquals(set2);
                if (equals == false && set.Count() == set2.Count())
                    teachersFree.Add(s);
            }
            scheduleList = scheduleList.Except(teachersFree).ToList();
            return scheduleList;
        }

    }
}
