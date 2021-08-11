using LabraDog.DAL;
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
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ShopListViewModel shopListVM = new ShopListViewModel()
            {
                Products = _context.Products
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id).ToList(),
                Categories = _context.Categories.ToList(),

            };
            return View(shopListVM);
        }

        [HttpGet]
        public IActionResult ShopDetail(int id)
        {
            Product product = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ReviewProducts).ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return RedirectToAction("index", "shop");
            }
            ShopDetailViewModel shopDetailVM = new ShopDetailViewModel
            {
                Product = product,
                LastProducts = _context.Products.OrderByDescending(x => x.Id).Take(4).ToList(),
            };
            return View(shopDetailVM);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(ReviewProduct review)
        {
            if (!ModelState.IsValid) return RedirectToAction("shopdetail", new { id = review.ProductId });

            Product product = _context.Products.Include(x => x.ReviewProducts).FirstOrDefault(x => x.Id == review.ProductId);

            if (product == null) return RedirectToAction("index");

            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            review.UserId = user.Id;

            //if (_context.ReviewProduct.Any(x => x.ProductId == review.ProductId && x.UserId == user.Id))
            //{
            //    return RedirectToAction("index");
            //}

            review.CreatedAt = DateTime.UtcNow.AddHours(4);
            _context.ReviewProducts.Add(review);

            _context.SaveChanges();

            return RedirectToAction("shopdetail", new { id = review.ProductId });

        }
    }
}
