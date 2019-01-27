using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab11MVC.Models;

namespace Lab11MVC.Controllers
{
    public class HomeController: Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int startYear, int endYear)
        {
            return RedirectToAction("Results", new { startYear, endYear });
        }

        [HttpGet]
        public IActionResult Results(int startYear, int endYear)
        {
            return View(TimePerson.GetPersons(startYear, endYear));
        }
    }
}
