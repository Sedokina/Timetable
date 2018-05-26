using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeneratorLogic;
using GeneratorServiceServer;

namespace TimetableWebApp.Controllers
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
            GeneratorServiceImpl gr = new GeneratorServiceImpl();
            return View(gr.GetFacultiesView().OrderBy(f => f.Name));
        }

        public ActionResult Timetable(int id)
        {
            GeneratorServiceImpl gr = new GeneratorServiceImpl();
            return View(gr.GetGroupScheduleView(id));
        }

        public ActionResult Generate()
        {
            Generator gr = new Generator();
            gr.Generate();
            ViewBag.result = "Schedule generated";
            return View();
        }
    }
}