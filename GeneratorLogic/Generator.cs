﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GeneratorServiceServer;
using GeneratorService.Models;

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
            List<LoadData> Loads = services.GetGroupsLoadForCurrentSemester();
            List<int> teachers = UniqueTeachers(Loads);
            TeachersRate(teachers);
            //services.GetTeachersWeight();

            var query = Loads.Join(services.GetTeachersWeight(), l => l.TeacherId, w => w.Id, (l, w) =>
                new { load = l, TeacherWeight = w.Weight });

            var LoadWithWeights = query.Join(services.GetSubjectClassWeight(), l => l.load.SubjectClassId, w => w.Id, (l, w) =>
                new { l.load, l.TeacherWeight, SubjectClassWeight = w.Weight });
            Console.WriteLine();
            //Критерии умножаются на приоритет и суммируются
            float SubjectClassPriority = 10;
            float TeacherPriority = 10;
            var LoadWeighted = LoadWithWeights.Select(l => new { l.load, FullWeight = l.SubjectClassWeight * SubjectClassPriority + 
                l.TeacherWeight * TeacherPriority }).OrderByDescending(l => l.FullWeight);
            //!!!!!!! надо исключить таймслоты по неделям, т.е. запихнуть сюда таймслоты которые доступны
            //для всех недель которые нужны для занятий
            foreach (var load in LoadWeighted)
            {
                var Timeslots = services.GetHours().SelectMany(x => services.GetDaysOfWeek().Where(d=>d.Number != 6).Select(y => new { HourId = x.Id, DayId = y.Id })).ToList();
                var PersonalTimeSlots = services.GetTeacherPersonalTime(load.load.TeacherId).Select(pt => new { HourId = pt.HourId, DayId = pt.DayOfWeekId });
                var TeacherSchedule = services.GetTeacherSchedule(load.load.TeacherId).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                var GroupShcedule = services.GetGroupSchedule(load.load.GroupId).Select(s => new { HourId = s.HourId, DayId = s.DayOfWeekId });
                Timeslots = Timeslots.Except(PersonalTimeSlots)
                    .Except(TeacherSchedule).Except(GroupShcedule).ToList();
                var DepartmentAuditoriums = services.GetDepartmentAuditoriumsForSubjectType(load.load.DepartmentId, load.load.SubjectTypeId);
                List<TimeslotsData> FinalTimeslots = DepartmentAuditoriums.SelectMany(a => Timeslots.Select(t => new TimeslotsData { HourId = t.HourId, DayId = t.DayId, AuditoriumId = a.Id })).ToList();
                //Исключение таймслотов аудиторий из доступных тайслотов
                List<TimeslotsData> AuditoriumSchedule = new List<TimeslotsData>();
                DepartmentAuditoriums.ToList().ForEach(a => AuditoriumSchedule.AddRange(services.GetAuditoriumSchedule(a.Id)
                  .Select(at => new TimeslotsData { HourId = at.HourId, DayId = at.DayOfWeekId, AuditoriumId = at.AuditoriumId }).ToList()));
                foreach(TimeslotsData ts in AuditoriumSchedule)
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

                List<TimeslotsCriteriaWeightData> timeslotsWeight = FinalTimeslots.Select(ts =>
                    new TimeslotsCriteriaWeightData {
                        AuditoriumId = ts.AuditoriumId,
                        DayId = ts.DayId,
                        HourId = ts.HourId
                    }).ToList();

                foreach (CriteriaData c in criteria)
                {

                    foreach (var criteriaList in UseCriteria(c, FinalTimeslots, load.load).ToList())
                    {
                        foreach(var timeslot in timeslotsWeight.Where(ts => ts.DayId == criteriaList.DayId &&
                            ts.HourId == criteriaList.HourId && ts.AuditoriumId == criteriaList.AuditoriumId).ToList())
                        {
                            timeslot.criteriaWeight.Add(new CriteriaWeightData
                            {
                                criteria = c,
                                Weight = criteriaList.Weight
                            });
                        }
                    }
                    Console.WriteLine();
                }
                
                foreach(TimeslotsCriteriaWeightData tw in timeslotsWeight)
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

        private List<TimeslotsWeightData> UseCriteria(CriteriaData criteria, List<TimeslotsData> timeslots, LoadData load)
        {
            List<CriteriaRateData> criteriaRate = new List<CriteriaRateData>();
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
            return new List<TimeslotsWeightData>();
        }

        private List<CriteriaRateData> CheckGap(LoadData load, List<TimeslotsData> timeslots)
        {
            var groupSchedule = services.GetGroupSchedule(load.GroupId).Join(services.GetHours(), s => s.HourId, h => h.Id, (s, h) => new { s.Id, s.DayOfWeekId, s.HourId, hourNumber = h.Number });
            var tslots = timeslots.Join(services.GetHours(), t => t.HourId, h => h.Id, (t, h) => new { t.HourId, t.DayId, t.AuditoriumId, HourNumber = h.Number });

            List<CriteriaRateData> rate = new List<CriteriaRateData>();
            foreach (var t in tslots)
            {
                int gap = groupSchedule.Where(s => s.DayOfWeekId == t.DayId)
                    .Where(s => s.hourNumber == t.HourNumber + 1 || s.hourNumber == t.HourNumber - 1).Count();
                rate.Add(new CriteriaRateData { timeslots = new TimeslotsData{ DayId = t.DayId, HourId = t.HourId, AuditoriumId = t.AuditoriumId }, Rate = gap });
            }
            
            return rate;
        }

        private List<CriteriaRateData> MoreThanOneSubject(LoadData load, List<TimeslotsData> timeslots)
        {
            List<CriteriaRateData> rate = new List<CriteriaRateData>();
            var groupSchedule = services.GetGroupSchedule(load.GroupId);
            foreach(var day in services.GetDaysOfWeek())
            {
                if (groupSchedule.Where(gs => gs.DayOfWeekId == day.Id).Count() <= 1)
                {
                    rate.AddRange(timeslots.Where(ts => ts.DayId == day.Id).Select(ts => new CriteriaRateData
                    {
                        timeslots = new TimeslotsData
                        {
                            DayId = ts.DayId, HourId = ts.HourId, AuditoriumId = ts.AuditoriumId
                        }
                    , Rate = 1
                    }));
                }
            }

            return rate;
        }

        private List<CriteriaRateData> LessThenFourSubject(LoadData load, List<TimeslotsData> timeslots)
        {
            List<CriteriaRateData> rate = new List<CriteriaRateData>();
            var groupSchedule = services.GetGroupSchedule(load.GroupId);
            int rateValue = 0;
            foreach (var day in services.GetDaysOfWeek())
            {
                if (groupSchedule.Where(gs => gs.DayOfWeekId == day.Id).Count() >= 4)
                    rateValue = 0;
                else
                    rateValue = 1;
                rate.AddRange(timeslots.Where(ts => ts.DayId == day.Id).Select(ts => new CriteriaRateData
                {
                    timeslots = new TimeslotsData
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

        private List<CriteriaRateData> ScheduleShift(LoadData load, List<TimeslotsData> timeslots)
        {
            var slots = timeslots.Join(services.GetHours(), t => t.HourId, h => h.Id, (t, h) => new { t.DayId, t.HourId, t.AuditoriumId, h.Number });
            List<CriteriaRateData> rate = new List<CriteriaRateData>();
            var groupSchedule = services.GetGroupSchedule(load.GroupId);
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
                rate.Add(new CriteriaRateData
                {
                    timeslots = new TimeslotsData
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
        

        private List<int> UniqueTeachers(List<LoadData> Load)
        {
            return Load.Select(load => load.TeacherId)
                .GroupBy(l=>l).Select(y => y.First()).ToList();
        }

        private void TeachersRate(List<int> teachers)
        {
            services.InsertGenTeachers(teachers.Select(t => new TeacherData { Id = t }).ToList());
        }
    }
}
