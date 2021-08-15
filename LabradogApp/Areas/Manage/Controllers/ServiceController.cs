using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;


        public ServiceController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 5m);

            List<Service> model = _context.Services.Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ServiceCreateDto serviceCreate)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Service service = new Service();
            service.Title = serviceCreate.Title;
            service.Main = serviceCreate.Main;
            service.Image = FileManager.Save(serviceCreate.Image);
            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);

            if (service == null) return RedirectToAction("index");

            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(int id, ServiceCreateDto service)
        {
            if (!ModelState.IsValid) return View();

            Service existService = _context.Services.FirstOrDefault(x => x.Id == id);

            if (existService == null) return RedirectToAction("index");

            existService.Title = service.Title;
            existService.Main = service.Main;
            if (service.Image != null)
            {
                existService.Image = FileManager.Save(service.Image);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);

            if (service == null) return RedirectToAction("index");

            _context.Services.Remove(service);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
