using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            Blog blog = _context.Blogs.Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.ReviewBlogs).ThenInclude(x => x.User)
                .Include(x => x.BlogTags).ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null)
            {
                return RedirectToAction("blogs", "blog");
            }
            BlogDetailViewModel bookDetailVM = new BlogDetailViewModel
            {
                Blog = blog,
                Blogs = _context.Blogs.OrderByDescending(x => x.Id).Take(3).ToList(),
                Categories = _context.Categories.OrderByDescending(x => x.Id).Take(5).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
            };
            return View(bookDetailVM);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(ReviewBlog review)
        {
            if (!ModelState.IsValid) return RedirectToAction("blogdetail", new { id = review.BlogId });

            Blog blog = _context.Blogs.Include(x => x.ReviewBlogs).FirstOrDefault(x => x.Id == review.BlogId);

            if (blog == null) return RedirectToAction("blogs");

            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            review.UserId = user.Id;

            //if (_context.ReviewBlogs.Any(x => x.BlogId == review.BlogId && x.UserId == user.Id))
            //{
            //    return RedirectToAction("blogs");
            //}

            review.CreatedAt = DateTime.UtcNow.AddHours(4);
            _context.ReviewBlogs.Add(review);

            _context.SaveChanges();

            return RedirectToAction("blogdetail", new { id = review.BlogId });

        }

    }
}
