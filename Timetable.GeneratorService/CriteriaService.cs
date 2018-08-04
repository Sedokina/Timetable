using System;
using System.Collections.Generic;
using System.Text;
using Timetable.DAL;
using Timetable.DomainCore.Domain;

namespace Timetable.GeneratorService
{
    public class CriteriaService
    {
        
        public bool CriteriaEdit(Criteria criteria)
        {
            using (var db = new ScheduleKSTUContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Update(criteria);
                        db.SaveChanges();
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
    }
}
