using LabraDog.DAL;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabradogApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;


        public OrderController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }


        [Route("/checkout")]
        public IActionResult Checkout()
        {
            var productStr = HttpContext.Request.Cookies["basket"];
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

        [HttpPost]
        public IActionResult Create(Order order)
        {
            User user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.isAdmin);
            }

            if (user == null)
            {
                ViewBag.IsAuthenticated = false;

                if (string.IsNullOrWhiteSpace(order.FullName) || string.IsNullOrWhiteSpace(order.ContactPhone) || string.IsNullOrWhiteSpace(order.Adress))
                {
                    ModelState.AddModelError("", "FullName, ContactPhone ve Address deyerleri gonderilmelidir!");
                    return View("Checkout", _getBasketItems());
                }
            }
            else
            {
                order.FullName = user.FullName;
                order.ContactPhone = user.PhoneNumber;
            }


            List<ProductBasketItem> cookieBasketItems = new List<ProductBasketItem>();
            cookieBasketItems = JsonConvert.DeserializeObject<List<ProductBasketItem>>(HttpContext.Request.Cookies["basket"]);

            order.OrderProducts = new List<OrderProduct>();
            order.Status = Enums.OrderStatus.Pending;
            order.CreatedAt = DateTime.UtcNow;
            order.UserId = user != null ? user.Id : null;


            foreach (var item in cookieBasketItems)
            {
                Product product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == item.Id);
                OrderProduct orderItem = new OrderProduct
                {
                    ProductId = product.Id,
                    Count = item.Count,
                    SalePrice = product.DiscountPrice,
                    CategoryName = product.Category.Name,
                    Name = product.Name,
                };
                orderItem.TotalPrice = orderItem.Price * orderItem.Count;
                order.OrderProducts.Add(orderItem);

                order.TotalPrice += orderItem.TotalPrice;
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            HttpContext.Response.Cookies.Delete("basket");
            return RedirectToAction("index", "home");
        }

        private List<BasketItemViewModel> _getBasketItems()
        {
            List<ProductBasketItem> cookieBasketItems = new List<ProductBasketItem>();
            cookieBasketItems = JsonConvert.DeserializeObject<List<ProductBasketItem>>(HttpContext.Request.Cookies["basket"]);

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

            return basketItems;
        }
    }
}
