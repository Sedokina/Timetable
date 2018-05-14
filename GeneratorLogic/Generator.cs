using System;
using System.Collections.Generic;
using System.Text;
using DataModel.Models;
using System.Linq;
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
            TeachersRate(teachers);
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

        #region Get
        
        #endregion
    }
}
