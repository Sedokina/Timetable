using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GeneratorServiceServer;
using DomainModel.Domain;
using DomainModel.Models;

namespace GeneratorLogic
{
    public class Generator
    {
        GeneratorServiceImpl services;
        public Generator()
        {
            services = new GeneratorServiceImpl();
            services.ClearAllGenData();
        }

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
            List<Raschasovka> Loads = services.GetGroupsLoadForCurrentSemester();
            List<RateData> teachers = UniqueTeachers(Loads).Select(t => new RateData { Id = t }).ToList();
            //Оценка личного времени преподавателей преподавателей
            TeachersRatePersonalTime(teachers);
            //services.GetTeachersWeight();

            //Выборка критерия личного времени
            var query = Loads.Join(services.GetTeachersWeight(), l => l.TeacherId, w => w.Id, (l, w) =>
                new QueryClass { load = l, TeacherWeight = w.Weight }).ToList();
            //Оценка по критерию нагруженности из расчасовки
            TeachersRate(services.GetTeachersLoadRate());
            //Выборка по критерию нагруженности
            var query2 = query.Join(services.GetTeachersWeight(), l => l.load.TeacherId, w => w.Id, (l, w) =>
                new { l.load, l.TeacherWeight, TeachersLoadWeight = w.Weight });
            //Выборка критерия типа предмета
            var LoadWithWeights = query2.Join(services.GetSubjectClassWeight(), l => l.load.SubjectClassId, w => w.Id, (l, w) =>
                new { l.load, l.TeacherWeight, l.TeachersLoadWeight, SubjectClassWeight = w.Weight });
            Console.WriteLine();
            //Критерии умножаются на приоритет и суммируются
            float SubjectClassPriority = 10;
            float TeacherPriority = 10;
            float TeachersLoadPriority = 10;
            var LoadWeighted = LoadWithWeights.Select(l => new { l.load, FullWeight = l.SubjectClassWeight * SubjectClassPriority +
                l.TeacherWeight * TeacherPriority + l.TeachersLoadWeight * TeachersLoadPriority }).OrderByDescending(l => l.FullWeight);
            //!!!!!!! надо исключить таймслоты по неделям, т.е. запихнуть сюда таймслоты которые доступны
            
            //для всех недель которые нужны для занятий
            foreach (var load in LoadWeighted)
            {
                var Timeslots = services.GetHours().SelectMany(x => services.GetDaysOfWeek().Where(d=>d.Number != 6).Select(y => new { HourId = x.Id, DayId = y.Id })).ToList();
                var PersonalTimeSlots = services.GetTeacherPersonalTime(load.load.TeacherId).Select(pt => new { HourId = pt.HourId, DayId = pt.DayOfWeekId });
               //Исключить занятые таймслоты преподавателя
                var TeacherSchedule = services.GetTeacherSchedule(load.load).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                //Исключить занятые таймслоты группы
                var GroupShcedule = services.GetGroupSchedule(load.load).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                Timeslots = Timeslots.Except(PersonalTimeSlots)
                    .Except(TeacherSchedule).Except(GroupShcedule).ToList();
                var DepartmentAuditoriums = services.GetDepartmentAuditoriumsForSubjectType(load.load.DepartmentId, load.load.SubjectTypeId);
                List<Timeslots> FinalTimeslots = DepartmentAuditoriums.SelectMany(a => Timeslots.Select(t => new Timeslots { HourId = t.HourId, DayId = t.DayId, AuditoriumId = a.Id })).ToList();
                //Исключение таймслотов аудиторий из доступных тайслотов
                List<Timeslots> AuditoriumSchedule = new List<Timeslots>();
                DepartmentAuditoriums.ToList().ForEach(a => AuditoriumSchedule.AddRange(services.GetAuditoriumSchedule(a.Id)
                  .Select(at => new Timeslots { HourId = at.HourId, DayId = at.DayOfWeekId, AuditoriumId = at.AuditoriumId }).ToList()));
                foreach(Timeslots ts in AuditoriumSchedule)
                {
                    FinalTimeslots.Remove(FinalTimeslots.FirstOrDefault(ft => ft.DayId == ts.DayId && ft.HourId == ts.HourId && ft.AuditoriumId == ts.AuditoriumId));
                }
                //DepartmentAuditoriums.ToList().ForEach(a => FinalTimeslots = FinalTimeslots.Except(services.GetAuditoriumSchedule(a.Id)
                //   .Select(at => new Timeslots { HourId = at.HourId, DayId = at.DayOfWeekId, AuditoriumId = at.AuditoriumId })).ToList());

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

                List<TimeslotsCriteriaWeight> timeslotsWeight = FinalTimeslots.Select(ts =>
                    new TimeslotsCriteriaWeight {
                        AuditoriumId = ts.AuditoriumId,
                        DayId = ts.DayId,
                        HourId = ts.HourId
                    }).ToList();

                foreach (Criteria c in criteria)
                {

                    foreach (var criteriaList in UseCriteria(c, FinalTimeslots, load.load).ToList())
                    {
                        foreach(var timeslot in timeslotsWeight.Where(ts => ts.DayId == criteriaList.DayId &&
                            ts.HourId == criteriaList.HourId && ts.AuditoriumId == criteriaList.AuditoriumId).ToList())
                        {
                            timeslot.criteriaWeight.Add(new CriteriaWeight
                            {
                                criteria = c,
                                Weight = criteriaList.Weight
                            });
                        }
                    }
                    Console.WriteLine();
                }
                
                foreach(TimeslotsCriteriaWeight tw in timeslotsWeight)
                {
                    Console.WriteLine(tw.AllCriteriaWeight);
                    tw.criteriaWeight.ForEach(cw => tw.AllCriteriaWeight += cw.Weight * cw.criteria.Rate);
                }
                timeslotsWeight = timeslotsWeight.OrderByDescending(tw => tw.AllCriteriaWeight).ThenBy(tw=>tw.DayId).ThenBy(tw=>tw.HourId).ToList();
                services.AddScheduleWeeks(services.AddScheduleTimeslot(load.load, timeslotsWeight.FirstOrDefault()), load.load.Id);
                
                Console.WriteLine();
            }
            

            Console.WriteLine();
        }

        private List<TimeslotsWeight> UseCriteria(Criteria criteria, List<Timeslots> timeslots, Raschasovka load)
        {
            List<CriteriaRate> criteriaRate = new List<CriteriaRate>();
            switch (criteria.Name)
            {
                case "MinGap":
                    criteriaRate = CheckGap(load, timeslots);
                    break;
                case "MoreThanOne":
                    criteriaRate = MoreThanOneSubject(load, timeslots);
                    break;
                case "LessThenFour":
                    criteriaRate = LessThenFourSubject(load, timeslots);
                    break;
                case "Shift":
                    criteriaRate = ScheduleShift(load, timeslots);
                    break;
                default:
                    break;
            }

            if (criteriaRate.Count != 0)
            {
                services.InsertGenTimeslots(criteriaRate, load.Id);
                return services.GetTimeslotsWeight();
            }
            return new List<TimeslotsWeight>();
        }

        private List<CriteriaRate> CheckGap(Raschasovka load, List<Timeslots> timeslots)
        {
            var groupSchedule = services.GetGroupSchedule(load).Join(services.GetHours(), s => s.HourId, h => h.Id, (s, h) => new { s.Id, s.DayOfWeekId, s.HourId, hourNumber = h.Number }); //<==
            var tslots = timeslots.Join(services.GetHours(), t => t.HourId, h => h.Id, (t, h) => new { t.HourId, t.DayId, t.AuditoriumId, HourNumber = h.Number });

            List<CriteriaRate> rate = new List<CriteriaRate>();
            foreach (var t in tslots)
            {
                int gap = groupSchedule.Where(s => s.DayOfWeekId == t.DayId)
                    .Where(s => s.hourNumber == t.HourNumber + 1 || s.hourNumber == t.HourNumber - 1).Count();
                rate.Add(new CriteriaRate { timeslots = new Timeslots{ DayId = t.DayId, HourId = t.HourId, AuditoriumId = t.AuditoriumId }, Rate = gap });
            }
            
            return rate;
        }

        private List<CriteriaRate> MoreThanOneSubject(Raschasovka load, List<Timeslots> timeslots)
        {
            List<CriteriaRate> rate = new List<CriteriaRate>();
            var groupSchedule = services.GetGroupSchedule(load); //<==
            foreach(var day in services.GetDaysOfWeek())
            {
                if (groupSchedule.Where(gs => gs.DayOfWeekId == day.Id).Count() <= 1)
                {
                    rate.AddRange(timeslots.Where(ts => ts.DayId == day.Id).Select(ts => new CriteriaRate
                    {
                        timeslots = new Timeslots
                        {
                            DayId = ts.DayId, HourId = ts.HourId, AuditoriumId = ts.AuditoriumId
                        }
                    , Rate = 1
                    }));
                }
            }

            return rate;
        }

        private List<CriteriaRate> LessThenFourSubject(Raschasovka load, List<Timeslots> timeslots)
        {
            List<CriteriaRate> rate = new List<CriteriaRate>();
            var groupSchedule = services.GetGroupSchedule(load); //<==
            int rateValue = 0;
            foreach (var day in services.GetDaysOfWeek())
            {
                if (groupSchedule.Where(gs => gs.DayOfWeekId == day.Id).Count() >= 4)
                    rateValue = 0;
                else
                    rateValue = 1;
                rate.AddRange(timeslots.Where(ts => ts.DayId == day.Id).Select(ts => new CriteriaRate
                {
                    timeslots = new Timeslots
                    {
                        DayId = ts.DayId,
                        HourId = ts.HourId,
                        AuditoriumId = ts.AuditoriumId
                    }
                    ,
                    Rate = rateValue
                }));
            }
            return rate;
        }

        private List<CriteriaRate> ScheduleShift(Raschasovka load, List<Timeslots> timeslots)
        {
            var slots = timeslots.Join(services.GetHours(), t => t.HourId, h => h.Id, (t, h) => new { t.DayId, t.HourId, t.AuditoriumId, h.Number });
            List<CriteriaRate> rate = new List<CriteriaRate>();
            var groupSchedule = services.GetGroupSchedule(load); //<== groupid
            int rateValue = 0;
            int hoursPerDay = services.GetHours().Count() / 2;
            foreach(var t in slots)
            {
                if (GetShift(load.CourseId) == 1)
                {
                    if (t.Number <= hoursPerDay)
                        rateValue = 1;
                    else
                        rateValue = 0;
                }
                else
                {
                    if (t.Number > hoursPerDay)
                        rateValue = 1;
                    else
                        rateValue = 0;
                }
                rate.Add(new CriteriaRate
                {
                    timeslots = new Timeslots
                    {
                        DayId = t.DayId,
                        HourId = t.HourId,
                        AuditoriumId = t.AuditoriumId
                    },
                    Rate = rateValue
                });
            }
            return rate;
        }

        private int GetShift(int course)
        {
            if (course % 2 == 0)
                return 2;
            else
                return 1;
        }

        //Список групп из расчасовки за текущий семетр отсортированных по курсу
        

        private List<int> UniqueTeachers(List<Raschasovka> Load)
        {
            return Load.Select(load => load.TeacherId)
                .GroupBy(l=>l).Select(y => y.First()).ToList();
        }

        private void TeachersRate(List<RateData> teachersRate)
        {
            services.InsertGenTeachers(teachersRate);
        }

        public void TeachersRatePersonalTime(List<RateData> teachersRate)
        {
            teachersRate.ForEach(tr => tr.Rate = services.CountTeacherPersonalTime(tr.Id));
            TeachersRate(teachersRate);
        }
    }
}