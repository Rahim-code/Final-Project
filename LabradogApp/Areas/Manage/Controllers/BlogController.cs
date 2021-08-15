using LabraDog.DAL;
using LabradogApp.Areas.Manage.ViewModels;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("manage")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;


        public BlogController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 5m);

            List<Blog> model = _context.Blogs
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.ReviewBlogs).ThenInclude(x => x.User)
                .OrderByDescending(x => x.Created_At).Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(AddBlogDto blog)
        {
            User user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.isAdmin);
            }

            Blog newBlog = new Blog();
            newBlog.Name = blog.Name;
            newBlog.Image = FileManager.Save(blog.Image);
            newBlog.Description = blog.Description;
            newBlog.Description = blog.Description;
            newBlog.CategoryId = blog.CategoryId;
            newBlog.User = user;
            newBlog.Created_At = DateTime.Now;
            _context.Add(newBlog);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (blog == null)
            {
                return RedirectToAction("index");
            }

            UpdateBlogViewModel blogVM = new UpdateBlogViewModel()
            {
                Categories = _context.Categories.Where(x => x.Id != blog.CategoryId).ToList(),
                Blog = blog,
            };
            return View(blogVM);
        }

        [HttpPost]
        public IActionResult Edit(AddBlogDto blogDto, int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog == null)
            {
                return RedirectToAction("index");
            }
            blog.Name = blogDto.Name;
            if (blogDto.Image != null)
            {
                blog.Image = FileManager.Save(blogDto.Image);
            }
            blog.Description = blogDto.Description;
            blog.CategoryId = blogDto.CategoryId;
            _context.Update(blog);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog != null)
            {
                FileManager.Delete(blog.Image);
                _context.Remove(blog);
                _context.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
