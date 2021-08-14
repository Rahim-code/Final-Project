using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;


        public BlogController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

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


        [Route("/admin/bloglist")]
        [Authorize(Roles = "Admin")]
        public IActionResult BlogList()
        {
            List<Blog> blogs = _context.Blogs
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.ReviewBlogs).ThenInclude(x => x.User)
                .OrderByDescending(x => x.Created_At).ToList();
            return View(blogs);
        }

        [HttpGet]
        [Route("/admin/addblog")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBlog()
        {
            AddBlogViewModel blogVM = new AddBlogViewModel()
            {
                Categories = _context.Categories.ToList(),
                Tags = _context.Tags.ToList(),
            };
            return View(blogVM);
        }

        [HttpPost]
        [Route("/admin/addblog")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBlog(AddBlogDto blog)
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
            return RedirectToAction("bloglist");
        }

        [HttpGet]
        [Route("/admin/editblog")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBlog(int id)
        {
            Blog blog = _context.Blogs.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (blog == null)
            {
                return RedirectToAction("bloglist");
            }

            AddBlogViewModel blogVM = new AddBlogViewModel()
            {
                Categories = _context.Categories.Where(x => x.Id != blog.CategoryId).ToList(),
                Tags = _context.Tags.ToList(),
                Blog = blog,
            };
            return View(blogVM);
        }

        [HttpPost]
        [Route("/admin/editblog")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBlog(AddBlogDto blogDto, int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog == null)
            {
                return RedirectToAction("bloglist");
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
            return RedirectToAction("bloglist");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBlog(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog != null)
            {
                FileManager.Delete(blog.Image);
                _context.Remove(blog);
                _context.SaveChanges();
            }
            return RedirectToAction("bloglist");
        }
    }
}
