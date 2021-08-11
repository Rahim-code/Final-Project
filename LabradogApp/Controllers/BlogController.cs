using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/bloglist")]
        public IActionResult Blogs()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Blogs = _context.Blogs.Include(x => x.Category).Include(x => x.User).ToList(),
                Setting = _context.Settings.FirstOrDefault(),

            };
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult BlogDetail(int id)
        {
            Blog blog = _context.Blogs.Include(x=>x.User)
                .Include(x=>x.Category)
                .Include(x=>x.ReviewBlogs).ThenInclude(x=>x.Review)
                .Include(x=>x.BlogTags).ThenInclude(x=>x.Tag)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null)
            {
                return RedirectToAction("blogs","blog");
            }
            BlogDetailViewModel bookDetailVM = new BlogDetailViewModel
            {
              Blog = blog,
              Blogs = _context.Blogs.OrderByDescending(x=>x.Id).Take(3).ToList(),
              Categories = _context.Categories.OrderByDescending(x=>x.Id).Take(5).ToList(),
              Setting = _context.Settings.FirstOrDefault(),
            };
             return View(bookDetailVM);
        }
    }
}
