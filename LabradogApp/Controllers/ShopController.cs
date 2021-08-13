using LabraDog.DAL;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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


        public IActionResult Index(string search, int? categoryId)
        {
            if (categoryId == null)
            {
                ShopListViewModel shopListVM = new ShopListViewModel()
                {
                    Products = _context.Products
                    .Include(x => x.Category)
                    .Where(x => string.IsNullOrWhiteSpace(search) ? true : (x.Name.ToLower().Contains(search.ToLower())))
                    .OrderByDescending(x => x.Id).ToList(),
                    Categories = _context.Categories.ToList(),

                };
                return View(shopListVM);
            }
            else
            {
                ShopListViewModel shopListVM = new ShopListViewModel()
                {
                    Products = _context.Products
                   .Include(x => x.Category)
                   .Where(x => string.IsNullOrWhiteSpace(search) ? true : (x.Name.ToLower().Contains(search.ToLower())))
                   .Where(x => x.CategoryId == categoryId)
                   .OrderByDescending(x => x.Id).ToList(),
                    Categories = _context.Categories.ToList(),
                };
                return View(shopListVM);
            }
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

        [Route("/basket")]
        public IActionResult Basket()
        {
            var productStr = HttpContext.Request.Cookies["basket"];
            if (productStr != null)
            {
                List<ProductBasketItem> cookieBasketItems = JsonConvert.DeserializeObject<List<ProductBasketItem>>(productStr);
                List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();

                foreach (var item in cookieBasketItems)
                {
                    Product product = _context.Products.FirstOrDefault(x => x.Id == item.Id);
                    BasketItemViewModel basketItem = new BasketItemViewModel
                    {
                        Id = product.Id,
                        Count = item.Count,
                        Name = product.Name,
                        Price = product.DiscountPrice,
                        Image = product.Image,
                        Title = product.Title
                    };
                    basketItem.TotalPrice = basketItem.Price * basketItem.Count;

                    basketItems.Add(basketItem);
                }

                return View(basketItems);
            }
            return RedirectToAction("index");
        }

        public IActionResult AddBasket(int id)
        {

            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return Json(new { isSucceeded = false });

            List<ProductBasketItem> productBaskets = new List<ProductBasketItem>();
            ProductBasketItem productBasketItem = new ProductBasketItem
            {
                Id = product.Id,
                Count = 1
            };

            if (HttpContext.Request.Cookies["basket"] == null)
            {
                productBaskets.Add(productBasketItem);
            }
            else
            {

                productBaskets = JsonConvert.DeserializeObject<List<ProductBasketItem>>(HttpContext.Request.Cookies["basket"]);

                var existBasketItem = productBaskets.FirstOrDefault(x => x.Id == product.Id);

                if (existBasketItem != null)
                {
                    existBasketItem.Count += 1;
                }
                else
                {
                    productBaskets.Add(productBasketItem);
                }
            }

            decimal totalPrice = 0;
            foreach (var item in productBaskets)
            {
                Product pr = _context.Products.FirstOrDefault(x => x.Id == item.Id);
                decimal salePrice = pr.Price;
                totalPrice += salePrice * item.Count;
            }


            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(productBaskets, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            return RedirectToAction("basket");
        }
    }
}
