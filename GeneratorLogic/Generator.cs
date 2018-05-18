using System;
using System.Collections.Generic;
using System.Text;
using DataModel.Models;
using System.Linq;
using GeneratorLogic.Models;
namespace GeneratorLogic
{
    public class Generator
    {
        GeneratorServices services;
        public Generator() { services = new GeneratorServices(); }

        public bool Generate()
        {
            TimetableFlaws();
            TimetableGroups();
            return false;
        }

        private void TimetableFlaws()
        {
            services.GetFlawsFromLoad();
        }

        private void TimetableGroups()
        {
            List<Raschasovka> Loads = GetGroupsLoadForCurrentSemester();
            List<Teacher> teachers = UniqueTeachers(Loads);
            //TeachersRate(teachers);
            services.GetTeachersWeight();

            var query = Loads.Join(services.GetTeachersWeight(), l => l.TeacherId, w => w.Id, (l, w) =>
                new { load = l, TeacherWeight = w.Weight });

            var LoadWithWeights = query.Join(services.GetSubjectClassWeight(), l => l.load.SubjectClassId, w => w.Id, (l, w) =>
                new { l.load, l.TeacherWeight, SubjectClassWeight = w.Weight });

            //Критерии умножаются на приоритет и суммируются
            float SubjectClassPriority = 10;
            float TeacherPriority = 10;
            var LoadWeighted = LoadWithWeights.Select(l => new { l.load, FullWeight = l.SubjectClassWeight * SubjectClassPriority + 
                l.TeacherWeight * TeacherPriority }).OrderByDescending(l => l.FullWeight);

            foreach (var load in LoadWeighted)
            {
                var Timeslots = services.GetHours().SelectMany(x => services.GetDaysOfWeek().Select(y => new { HourId = x.Id, DayId = y.Id })).ToList();
                var PersonalTimeSlots = services.GetTeacherPersonalTime(load.load.TeacherId).Select(pt => new { HourId = pt.HourId, DayId = pt.DayOfWeekId });
                var TeacherSchedule = services.GetTeacherSchedule(load.load.TeacherId).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                var GroupShcedule = services.GetGroupSchedule(load.load.GroupId).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                Timeslots = Timeslots.Except(PersonalTimeSlots)
                    .Except(TeacherSchedule).Except(GroupShcedule).ToList();
                var DepartmentAuditoriums = services.GetDepartmentAuditoriumsForSubjectType(load.load.DepartmentId, load.load.SubjectTypeId);
                List<Timeslots> FinalTimeslots = DepartmentAuditoriums.SelectMany(a => Timeslots.Select(t => new Timeslots { HourId = t.HourId, DayId = t.DayId, AuditoriumId = a.Id })).ToList();
                //Исключение таймслотов аудиторий из доступных тайслотов
                DepartmentAuditoriums.ToList().ForEach(a => FinalTimeslots.Except(services.GetAuditoriumSchedule(a.Id)
                   .Select(at => new Timeslots { HourId = at.HourId, DayId = at.DayOfWeekId, AuditoriumId = at.AuditoriumId })));

                //Если часов для занятия > 2, то исключить таймслоты без смежных пустых таймслотов
                //потом изменить под недели
                if (load.load.TotalHoursForSemestr / 16 > 2)
                {
                    foreach(var ts in FinalTimeslots)
                    {

                    }
                }

                //Если есть рекомендованная аудитория
                if (load.load.AuditoriumId != null)
                    ;


                var criteria = services.GetCriteria();

                foreach(Criteria c in criteria)
                {
                    UseCriteria(c, FinalTimeslots, load.load);
                }

            }
            

            Console.WriteLine();
        }

        private void UseCriteria(Criteria criteria, List<Timeslots> timeslots, Raschasovka load)
        {
            List<CriteriaRate> criteriaRate;
            switch (criteria.Name)
            {
                case "MinGap":
                    criteriaRate = CheckGap(load, timeslots);
                    break;
                case "MoreThenOne":
                    break;
                case "LessThenFour":
                    break;
                default:
                    break;
            }
            //services.InsertGenTimeslots();
        }

        private List<CriteriaRate> CheckGap(Raschasovka load, List<Timeslots> timeslots)
        {
            var groupSchedule = services.GetGroupSchedule(load.GroupId);
            var tslots = timeslots.Join(services.GetHours(), t => t.HourId, h => h.Id, (t, h) => new { t.HourId, t.DayId, t.AuditoriumId, HourNumber = h.Number });
            List<CriteriaRate> rate = new List<CriteriaRate>();
            foreach (var t in tslots)
            {
                int gap = groupSchedule.Where(s => s.DayOfWeekId == t.DayId)
                    .Where(s => s.Hour.Number == t.HourNumber + 1 || s.Hour.Number == t.HourNumber - 1).Count();
                rate.Add(new CriteriaRate { timeslots = new Timeslots{ DayId = t.DayId, HourId = t.HourId, AuditoriumId = t.AuditoriumId }, Rate = gap });
            }
            
            return rate;
        }


        //Список групп из расчасовки за текущий семетр отсортированных по курсу
        private List<Raschasovka> GetGroupsLoadForCurrentSemester()
        {

            return services.GetLoad().Where(load => load.Semester == services.GetSemesterByName("Весенний")).
                OrderBy(load => load.Course.Number).ToList();
        }

        private List<Teacher> UniqueTeachers(List<Raschasovka> Load)
        {
            return Load.Select(load => load.Teacher)
                .GroupBy(l => l.Id).Select(y => y.First()).ToList();
        }

        private void TeachersRate(List<Teacher> teachers)
        {
            services.InsertGenTeachers(teachers);
        }

        //List<Raschasovka> r = db.Raschasovka.ToList();
        //List<TableWeight> w = db.Set<TableWeight>().FromSql("dbo.GetTeachersWeight").ToList();
        //var query = r.Join(w, ras => ras.TeacherId, wei => wei.Id, (ras, wei) => new { Raschasovka = ras, TableWeight = wei });

        #region Get

        #endregion
    }
}
