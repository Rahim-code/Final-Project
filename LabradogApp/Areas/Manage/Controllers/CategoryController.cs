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
    [Area("manage")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Products.Count() / 5m);

            var model = _context.Categories.Skip((page - 1) * 5).Take(5).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return RedirectToAction("index");

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (!ModelState.IsValid) return View();

            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (existCategory == null) return RedirectToAction("index");

            existCategory.Name = category.Name;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return RedirectToAction("index");


            List<Product> products = _context.Products.Where(x => x.CategoryId == id).ToList();

            _context.Products.RemoveRange(products);
            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
