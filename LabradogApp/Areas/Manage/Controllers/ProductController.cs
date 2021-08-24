using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;


        public ProductController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Products.Count() / 5m);

            List<Product> model = _context.Products.Include(x=>x.Category).Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Product product = new Product()
            {
                Name = productCreateDto.Name,
                Title = productCreateDto.Title,
                TypeOfMeat = productCreateDto.TypeOfMeat,
                FeedClass = productCreateDto.FeedClass,
                CountryOfOrigin = productCreateDto.CountryOfOrigin,
                BreedSize = productCreateDto.BreedSize,
                Image = FileManager.Save(productCreateDto.Image),
                Price= productCreateDto.Price,
                DiscountPrice= productCreateDto.DiscountPrice,
                CategoryId = productCreateDto.CategoryId,
                PetsAge = productCreateDto.PetsAge,
                DeliveryAndPaymentInfo = productCreateDto.DeliveryAndPaymentInfo,
                SpecialParameters = productCreateDto.SpecialParameters,
        };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = _context.Categories.Where(x=>x.Id != product.CategoryId).ToList();

            if (product == null) return RedirectToAction("index");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid) return View();

            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == id);

            if (existProduct == null) return RedirectToAction("index");

            existProduct.Name = productCreateDto.Name;
            existProduct.Title = productCreateDto.Title;
            existProduct.TypeOfMeat = productCreateDto.TypeOfMeat;
            existProduct.FeedClass = productCreateDto.FeedClass;
            existProduct.CountryOfOrigin = productCreateDto.CountryOfOrigin;
            existProduct.BreedSize = productCreateDto.BreedSize;
            if (productCreateDto.Image != null)
            {
              existProduct.Image = FileManager.Save(productCreateDto.Image);
            }
            existProduct.Price = productCreateDto.Price;
            existProduct.DiscountPrice = productCreateDto.DiscountPrice;
            existProduct.CategoryId = productCreateDto.CategoryId;
            existProduct.PetsAge = productCreateDto.PetsAge;
            existProduct.DeliveryAndPaymentInfo = productCreateDto.DeliveryAndPaymentInfo;
            existProduct.SpecialParameters = productCreateDto.SpecialParameters;

            _context.Products.Update(existProduct);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            List<OrderProduct> orderProducts = _context.OrderProducts.Where(x => x.ProductId == id).ToList();


            if (product == null) return RedirectToAction("index");

            _context.OrderProducts.RemoveRange(orderProducts);
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
