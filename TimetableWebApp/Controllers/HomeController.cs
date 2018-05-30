﻿using System;
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
            ViewBag.Faculties = gr.GetFacultiesView().OrderBy(f => f.Name);
            Microsoft.AspNetCore.Mvc.Rendering.SelectList weeks = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(gr.GetWeeks(), "Id", "Number");
            ViewBag.Weeks = weeks;
            return View();
        }
       
        public ActionResult Timetable(int id, byte weekId)
        {
            GeneratorServiceImpl gr = new GeneratorServiceImpl();
            return View(gr.GetGroupScheduleView(id, weekId, 1086));
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