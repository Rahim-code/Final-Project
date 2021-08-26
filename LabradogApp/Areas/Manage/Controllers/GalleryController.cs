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
    public class GalleryController : Controller
    {
        private readonly AppDbContext _context;


        public GalleryController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Images.Count() / 5m);

            List<Image> model = _context.Images.Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GalleryImageCreateDto image)
        {
            Image newImage = new Image();
            newImage.DogName = image.DogName;
            newImage.Old = image.Old;
            newImage.DogImage = FileManager.Save(image.DogImage);
            newImage.IsMale = image.IsMale;
            _context.Add(newImage);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Image image = _context.Images.FirstOrDefault(x => x.Id == id);

            if (image == null) return RedirectToAction("index");

            return View(image);
        }

        [HttpPost]
        public IActionResult Edit(int id, GalleryImageCreateDto image)
        {
            if (!ModelState.IsValid) return View();

            Image existImage = _context.Images.FirstOrDefault(x => x.Id == id);

            if (existImage == null) return RedirectToAction("index");

            existImage.Old = image.Old;
            existImage.DogName = image.DogName;
            existImage.IsMale = image.IsMale;
            if (image.DogImage != null)
            {
                existImage.DogImage = FileManager.Save(image.DogImage);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }



        public IActionResult Delete(int id)
        {
            Image image = _context.Images.FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                _context.Remove(image);
                _context.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
