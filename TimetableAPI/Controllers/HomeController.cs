using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneratorLogic;
namespace TimetableAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criteria()
        {
            return View();
        }

        public ActionResult Faculties()
        {
            GeneratorServices gs = new GeneratorServices();
            return View(gs.GetFaculties().OrderBy(f=>f.Name));
        }

        public ActionResult Timetable(int id)
        {
            GeneratorServices gs = new GeneratorServices();
            return View(gs.GetFacultySchedule(id));
        }
    }
}