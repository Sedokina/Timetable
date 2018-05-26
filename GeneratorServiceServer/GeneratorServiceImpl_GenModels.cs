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
        public List<TableWeightData> GetTeachersWeight()
        {
            return db.Set<TableWeight>().FromSql("dbo.GetTeachersWeight").Select(w => new TableWeightData
            {
                Id = w.Id,
                Weight = w.Weight
            }).ToList();
        }

        public List<TableWeightData> GetSubjectClassWeight()
        {
            return db.Set<TableWeight>().FromSql("dbo.GetSubjectClassWeight").Select(w => new TableWeightData
            {
                Id = w.Id,
                Weight = w.Weight
            }).ToList();
        }

        public List<TimeslotsWeightData> GetTimeslotsWeight()
        {
            return db.Set<TimeslotsWeight>().FromSql("dbo.GetTimeslotsWeight").Select(t => new TimeslotsWeightData
            {
                Id = t.Id,
                DayId = t.DayId,
                HourId = t.HourId,
                AuditoriumId = t.AuditoriumId,
                Weight = t.Weight
            }).ToList();
        }

        public bool GenTimeslotsClear()
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
            return true;
        }

        public bool GenTeachersClear()
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
            return true;
        }

        private bool GenSubjectClassClear()
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
            return true;
        }

        public bool ScheduleClear()
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
            return true;
        }

        public bool InsertGenTimeslots(List<CriteriaRateData> timeslots, int i)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                ///////!!!!!!!
                db.Database.ExecuteSqlCommand("Delete From Gen_Timeslots");

                try
                {
                    foreach (CriteriaRateData cr in timeslots)
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
