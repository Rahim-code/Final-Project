using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LabradogApp.Models;
using LabraDog.DAL;
using LabradogApp.ViewModels;

namespace LabradogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;


        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Setting = _context.Settings.FirstOrDefault(),
                Fags = _context.Fags.OrderByDescending(x => x.Id).Take(6).ToList(),
                DidYouNows = _context.DidYouNows.ToList().ToList(),
                Services = _context.Services.OrderByDescending(x => x.Id).Take(3).ToList(),
                Products = _context.Products.OrderByDescending(x => x.Id).Take(4).ToList(),
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("/contact")]
        public IActionResult Contact()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Setting = _context.Settings.FirstOrDefault(),
            };
            return View(homeVM);
        }

        [HttpPost]
        [Route("/contact")]
        public IActionResult Contact(ContactUs contactUs)
        {
            _context.ContactUs.Add(contactUs);
            _context.SaveChanges();
            return RedirectToAction("contact", "home");
        }


        [HttpGet]
        [Route("/about")]
        public IActionResult About()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Setting = _context.Settings.FirstOrDefault(),
                Teams = _context.Teams.OrderByDescending(x => x.Id).Take(4).ToList(),
                Services = _context.Services.OrderByDescending(x => x.Id).Take(3).ToList(),
            };
            return View(homeVM);
        }

        [HttpGet]
        [Route("/services")]
        public IActionResult Services()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Setting = _context.Settings.FirstOrDefault(),
                Teams = _context.Teams.OrderByDescending(x => x.Id).Take(4).ToList(),
                Services = _context.Services.OrderByDescending(x => x.Id).ToList(),
                Fags = _context.Fags.OrderByDescending(x => x.Id).Take(8).ToList(),
            };
            return View(homeVM);
        }

        [HttpGet]
        [Route("/team")]
        public IActionResult Team()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Setting = _context.Settings.FirstOrDefault(),
                Teams = _context.Teams.OrderByDescending(x => x.Id).ToList(),
            };
            return View(homeVM);
        }

        [Route("/gallery")]
        public IActionResult Gallery()
        {
            List<Image> images = _context.Images.OrderByDescending(x => x.Id).ToList();
            return View(images);
        }

    }
}
