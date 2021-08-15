using LabraDog.DAL;
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
    public class FaqController : Controller
    {
        private readonly AppDbContext _context;


        public FaqController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 5m);

            List<Fag> model = _context.Fags.Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Fag fag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Fags.Add(fag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Fag fag = _context.Fags.FirstOrDefault(x => x.Id == id);

            if (fag == null) return RedirectToAction("index");

            return View(fag);
        }

        [HttpPost]
        public IActionResult Edit(int id, Fag fag)
        {
            if (!ModelState.IsValid) return View();

            Fag existFag = _context.Fags.FirstOrDefault(x => x.Id == id);

            if (existFag == null) return RedirectToAction("index");

            existFag.Question = fag.Question;
            existFag.Answer = fag.Answer;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Fag fag = _context.Fags.FirstOrDefault(x => x.Id == id);

            if (fag == null) return RedirectToAction("index");

            _context.Fags.Remove(fag);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
