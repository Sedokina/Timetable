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
        public List<Schedule> GetFacultySchedule(int facultyId)
        {
            return db.Schedule.ToList();
        }


        //public List<Schedule> GetGroupSchedule(long groupId)
        //{
        //    return db.Schedule
        //        .Include(s => s.ScheduleWeeks)
        //        .Where(s => s.GroupId == groupId && s.SemesterId == GetCurrentSemester().Id).ToList();
        //}

        public List<Schedule> GetGroupSchedule(Raschasovka load)
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

        public List<Schedule> GetGroupScheduleWeeks(Raschasovka load)
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

        public List<Schedule> GetAuditoriumSchedule(short auditoriumId)
        {
            return db.Schedule.Where(s => s.AuditoriumId == auditoriumId && s.SemesterId == GetCurrentSemester().Id).ToList();
        }

        public bool ScheduleWeeksClear()
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
            return true;
        }

        public int AddScheduleTimeslot(Raschasovka load, TimeslotsCriteriaWeight timeslot)
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

        public bool AddScheduleWeeks(int scheduleId, int loadId)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<ScheduleWeeks> scheduleWeeks = db.RaschasovkaWeeks.Where(rw => rw.RaschasovkaId == loadId && rw.HoursForWeek!=0)
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


        //public List<ScheduleViewModel> GetGroupScheduleViewSW(int facultyId, byte weekId, int groupId)
        //{

        //    return GetWeekFacultySchedule(facultyId, weekId)
        //        .Include(s => s.Auditorium)
        //        .Include(s => s.DayOfWeek)
        //        .Include(s => s.Group)
        //        .Include(s => s.Hour)
        //        .Include(s => s.Semester)
        //        .Include(s => s.Subject)
        //        .Include(s => s.SubjectType)
        //        .Include(s => s.Teacher)
        //        .Select(s => new ScheduleViewModel
        //        {
        //            Auditorium = new AuditoriumData
        //            {
        //                Id = s.Auditorium.Id,
        //                AuditoriumTypeId = s.Auditorium.AuditoriumTypeId,
        //                BuildingId = s.Auditorium.BuildingId,
        //                DepartmentId = s.Auditorium.DepartmentId,
        //                Name = s.Auditorium.Name,
        //                Number = s.Auditorium.Number,
        //                SeatingCapacity = s.Auditorium.SeatingCapacity
        //            },
        //            DayOfWeek = new DayOfWeekData
        //            {
        //                Id = s.DayOfWeek.Id,
        //                Name = s.DayOfWeek.Name,
        //                Number = s.DayOfWeek.Number,
        //                FullName = s.DayOfWeek.FullName
        //            },
        //            Group = new GroupData
        //            {
        //                Id = s.Group.Id,
        //                Name = s.Group.Name,
        //                DepartmentId = s.Group.DepartmentId,
        //                NumberOfStudents = s.Group.NumberOfStudents
        //            },
        //            Hour = new HourData
        //            {
        //                Id = s.Hour.Id,
        //                Begin = s.Hour.Begin,
        //                End = s.Hour.End,
        //                Number = s.Hour.Number
        //            },
        //            Semester = new SemestersData
        //            {
        //                Id = s.Semester.Id,
        //                Name = s.Semester.Name
        //            },
        //            Subject = new SubjectData
        //            {
        //                Id = s.Subject.Id,
        //                Name = s.Subject.Name,
        //                FullName = s.Subject.FullName
        //            },
        //            SubjectType = new SubjectTypeData
        //            {
        //                Id = s.SubjectType.Id,
        //                FullName = s.SubjectType.FullName,
        //                Name = s.SubjectType.Name
        //            },
        //            Teacher = new TeacherData
        //            {
        //                Id = s.Teacher.Id,
        //                FirstName = s.Teacher.FirstName,
        //                LastName = s.Teacher.LastName
        //            },
        //        })
        //        .ToList();
        //}

        public List<Schedule> GetWeekFacultySchedule(int facultyId, byte weekId)
        {
            return db.ScheduleWeeks
                .Include(sw => sw.Schedule)
                .ThenInclude(sw => sw.Group)
                .ThenInclude(sw => sw.Department)
                .Include(sw=>sw.Schedule.Subject)
                .Include(sw=>sw.Schedule.Auditorium)
                .Include(sw => sw.Schedule.DayOfWeek)
                .Include(sw => sw.Schedule.Hour)
                .Include(sw => sw.Schedule.SubjectType)
                .Include(sw => sw.Schedule.Teacher)
                .Where(sw => sw.Schedule.SemesterId == GetCurrentSemester().Id && sw.Schedule.Group.Department.FacultyId == facultyId && sw.WeekId == weekId)
                .Select(sw => new Schedule {
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
