
using LabraDog.DAL;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabradogApp.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("manage")]
    public class DidYouKnowController : Controller
    {
        private readonly AppDbContext _context;


        public DidYouKnowController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 5m);

            List<DidYouNow> model = _context.DidYouNows.Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(DidYouNow didYouNow)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.DidYouNows.Add(didYouNow);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            DidYouNow didYouNow = _context.DidYouNows.FirstOrDefault(x => x.Id == id);

            if (didYouNow == null) return RedirectToAction("index");

            return View(didYouNow);
        }

        [HttpPost]
        public IActionResult Edit(int id, DidYouNow didYouNow)
        {
            if (!ModelState.IsValid) return View();

            DidYouNow existDidYouKnow = _context.DidYouNows.FirstOrDefault(x => x.Id == id);

            if (existDidYouKnow == null) return RedirectToAction("index");

            existDidYouKnow.Topic = didYouNow.Topic;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            DidYouNow didYouNow = _context.DidYouNows.FirstOrDefault(x => x.Id == id);

            if (didYouNow == null) return RedirectToAction("index");

            _context.DidYouNows.Remove(didYouNow);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
