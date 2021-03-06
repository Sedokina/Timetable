﻿using Timetable.DomainCore.Domain;
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
        public List<TableWeight> GetTeachersWeight()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Set<TableWeight>().FromSql("dbo.GetTeachersWeight").AsNoTracking().ToList();
            }
        }

        public List<TableWeight> GetSubjectClassWeight()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Set<TableWeight>().FromSql("dbo.GetSubjectClassWeight").AsNoTracking().ToList();
            }
        }

        public List<TimeslotsWeight> GetTimeslotsWeight()
        {
            using (var db = new ScheduleKSTUContext())
            {
                return db.Set<TimeslotsWeight>().FromSql("dbo.GetTimeslotsWeight").AsNoTracking().ToList();
            }
        }

        public bool GenTimeslotsClear()
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("Truncate Table Gen_Timeslots");
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

        public bool GenTeachersClear()
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("Truncate Table Gen_Teachers");
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

        private bool GenSubjectClassClear()
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("Truncate Table Gen_SubjectClass");
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

        public bool ScheduleClear()
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("Delete From Schedule");
                        db.Database.ExecuteSqlCommand("DBCC CHECKIDENT(Schedule, RESEED, 0)");
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

        public bool InsertGenTimeslots(List<CriteriaRate> timeslots, int i)
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    ///////!!!!!!!
                    db.Database.ExecuteSqlCommand("Delete From Gen_Timeslots");

                    try
                    {
                        foreach (CriteriaRate cr in timeslots)
                        {

                            db.GenTimeslots.Add(new GenTimeslots
                            {
                                AuditoriumId = cr.timeslots.AuditoriumId,
                                DayId = cr.timeslots.DayId,
                                HourId = cr.timeslots.HourId,
                                Rate = cr.Rate + 1
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
            }
            return true;
        }

        public void ClearAllGenData()
        {
            GenTeachersClear();
            GenTimeslotsClear();
            ScheduleWeeksClear();
            ScheduleClear();
        }
    }
}
