using Timetable.DomainCore.Domain;
using Timetable.DomainCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timetable.DAL;

namespace Timetable.GeneratorService
{
    public partial class GeneratorServiceImpl
    {
        public List<Schedule> GetFacultySchedule(int facultyId)
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Schedule.ToList();
            }
        }


        //public List<Schedule> GetGroupSchedule(long groupId)
        //{
        //    return db.Schedule
        //        .Include(s => s.ScheduleWeeks)
        //        .Where(s => s.GroupId == groupId && s.SemesterId == GetCurrentSemester().Id).ToList();
        //}

        public List<Schedule> GetGroupSchedule(Raschasovka load)
        {
            using (var db = new ScheduleKSTUContext())
            {
                List<Schedule> scheduleList = db.Schedule.Include(s => s.ScheduleWeeks)
                .Where(s => s.GroupId == load.GroupId && s.SemesterId == GetCurrentSemester().Id).ToList();
                List<Schedule> groupFree = new List<Schedule>();
                foreach (var s in scheduleList)
                {
                    var set = new HashSet<int>(load.RaschasovkaWeeks.Where(rw => rw.HoursForWeek != 0).Select(w => (int)w.WeekId));
                    var set2 = s.ScheduleWeeks.Select(sw => (int)sw.WeekId);
                    var equals = set.SetEquals(set2);
                    if (equals == false && set.Count() == set2.Count())
                        groupFree.Add(s);
                }
                scheduleList = scheduleList.Except(groupFree).ToList();
                return scheduleList;
            }
        }

        public List<Schedule> GetGroupScheduleWeeks(Raschasovka load)
        {
            using (var db = new ScheduleKSTUContext())
            {
                bool weeksContains = false;
                var scheduleList = db.Schedule.Where(s => s.GroupId == load.GroupId && s.SemesterId == GetCurrentSemester().Id).ToList();
                List<Schedule> teachersFree = new List<Schedule>();
                foreach (var s in scheduleList)
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

        public List<Schedule> GetAuditoriumSchedule(short auditoriumId)
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Schedule.Where(s => s.AuditoriumId == auditoriumId && s.SemesterId == GetCurrentSemester().Id).ToList();
            }
        }

        public bool ScheduleWeeksClear()
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("Truncate Table ScheduleWeeks");
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            return true;
        }

        public int AddScheduleTimeslot(Raschasovka load, TimeslotsCriteriaWeight timeslot)
        {
            using (var db = new ScheduleKSTUContext())
            {
                Schedule schedule;
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        schedule = new Schedule
                        {
                            GroupId = load.GroupId,
                            TeacherId = load.TeacherId,
                            SubjectId = load.SubjectId,
                            SubjectTypeId = load.SubjectTypeId,
                            SemesterId = GetCurrentSemester().Id,
                            HourId = timeslot.HourId,
                            DayOfWeekId = timeslot.DayId,
                            AuditoriumId = timeslot.AuditoriumId,

                        };
                        db.Schedule.Add(schedule);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
                return schedule.Id;
            }
        }

        public bool AddScheduleWeeks(int scheduleId, int loadId)
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        List<ScheduleWeeks> scheduleWeeks = db.RaschasovkaWeeks.Where(rw => rw.RaschasovkaId == loadId && rw.HoursForWeek != 0)
                            .Select(rw => new ScheduleWeeks { ScheduleId = scheduleId, WeekId = rw.WeekId }).ToList();
                        db.ScheduleWeeks.AddRange(scheduleWeeks);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }

        public List<Schedule> GetGroupScheduleView(int facultyId, byte? weekId)
        {
            byte wid;
            if (weekId != null)
                wid = (byte)weekId;
            else
                wid = GetCurrentWeek();
            var list = GetWeekFacultySchedule(facultyId, wid);
            return list;
        }

        public List<Schedule> GetWeekFacultySchedule(int facultyId, byte weekId)
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.ScheduleWeeks
                .Include(sw => sw.Schedule)
                .ThenInclude(sw => sw.Group)
                .ThenInclude(sw => sw.Department)
                .Include(sw => sw.Schedule.Subject)
                .Include(sw => sw.Schedule.Auditorium)
                .Include(sw => sw.Schedule.DayOfWeek)
                .Include(sw => sw.Schedule.Hour)
                .Include(sw => sw.Schedule.SubjectType)
                .Include(sw => sw.Schedule.Teacher)
                .Where(sw => sw.Schedule.SemesterId == GetCurrentSemester().Id && sw.Schedule.Group.Department.FacultyId == facultyId && sw.WeekId == weekId)
                .Select(sw => new Schedule
                {
                    Id = sw.Schedule.Id,
                    AuditoriumId = sw.Schedule.AuditoriumId,
                    DayOfWeekId = sw.Schedule.DayOfWeekId,
                    GroupId = sw.Schedule.DayOfWeekId,
                    HourId = sw.Schedule.HourId,
                    SemesterId = sw.Schedule.SemesterId,
                    SubjectId = sw.Schedule.SubjectId,
                    SubjectTypeId = sw.Schedule.SubjectTypeId,
                    TeacherId = sw.Schedule.TeacherId,
                    Subject = sw.Schedule.Subject,
                    Auditorium = sw.Schedule.Auditorium,
                    DayOfWeek = sw.Schedule.DayOfWeek,
                    Group = sw.Schedule.Group,
                    Hour = sw.Schedule.Hour,
                    SubjectType = sw.Schedule.SubjectType,
                    Teacher = sw.Schedule.Teacher
                })
                .ToList();
            }
        }
    }
}
