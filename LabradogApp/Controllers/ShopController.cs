using LabraDog.DAL;
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
                .Include(x=>x.Category)
                .OrderByDescending(x => x.Id).ToList(),
                Categories = _context.Categories.ToList(),
            
            };
            return View(shopListVM);
        }

        [HttpGet]
        public IActionResult ShopDetail(int id)
        {
            Product product = _context.Products
                .Include(x=>x.Category)
                .FirstOrDefault(x=>x.Id == id);

            if (product == null)
            {
                return RedirectToAction("index", "shop");
            }
            ShopDetailViewModel shopDetailVM = new ShopDetailViewModel
            {
                Product = product,
                LastProducts = _context.Products.OrderByDescending(x=>x.Id).Take(4).ToList(),
            };
            return View(shopDetailVM);
        }
    }
}
