using LabraDog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LabraDog.Models.EntityFramework;
using LabradogApp.Models;

namespace LabradogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EfContext context = new EfContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Setting = context.Settings.FirstOrDefault();
            ViewBag.Fag = context.Fags.OrderByDescending(f => f.Id).Take(6);
            ViewBag.DidYouNow = context.DidYouNows.ToList();
            ViewBag.Service = context.Services.OrderByDescending(s => s.Id).Take(3);
            ViewBag.Product = context.Products.OrderByDescending(s => s.Id).Take(4);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/contact")]
        public IActionResult Contact()
        {
            ViewBag.Setting = context.Settings.FirstOrDefault();
            return View();
        }

        [HttpPost]
        [Route("/contact")]
        public IActionResult Contact(ContactUs contactUs)
        {
            context.ContactUs.Add(contactUs);
            context.SaveChanges();
            return RedirectToAction("contact","home");
        }


        [HttpGet]
        [Route("/about")]
        public IActionResult About()
        {
            ViewBag.Setting = context.Settings.FirstOrDefault();
            ViewBag.Service = context.Services.OrderByDescending(s => s.Id).Take(3);
            ViewBag.Staff = context.Teams.OrderByDescending(t => t.Id).Take(4);
            return View();
        }

        [HttpGet]
        [Route("/services")]
        public IActionResult Services()
        {
            ViewBag.Setting = context.Settings.FirstOrDefault();
            ViewBag.Service = context.Services.OrderByDescending(i=>i.Id).Take(3);
            ViewBag.Fag = context.Fags.OrderByDescending(t => t.Id).Take(8);
            return View();
        }

        [HttpGet]
        [Route("/team")]
        public IActionResult Team()
        {
            ViewBag.Setting = context.Settings.FirstOrDefault();
            ViewBag.Team = context.Teams.ToList();
            return View();
        }
    }
}
