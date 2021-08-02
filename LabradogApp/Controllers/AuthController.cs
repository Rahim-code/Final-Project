using LabraDog.Models;
using LabraDog.Models.EntityFramework;
using LabradogApp.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LabradogApp.Controllers
{
    public class AuthController : Controller
    {
        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string email, string password)
        {
            using (EfContext context = new EfContext())
            {
                var user = context.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        HttpContext.Session.SetString("email", email);
                        HttpContext.Session.SetString("name", user.Name);
                        HttpContext.Session.SetInt32("id", user.Id);
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        TempData["Errors"] = "Şifrə Yanlışdır";
                        return View();
                    }
                }
                else
                {
                    TempData["Errors"] = "Email Yanlışdır";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User userdata)
        {
            try
            {
                using (EfContext context = new EfContext())
                {
                    User user = new User();
                    user.Name = userdata.Name;
                    user.Email = userdata.Email;
                    user.Surname = userdata.Surname;
                    user.Password = userdata.Password;
                    user.Status = true;
                    user.Created_At = DateTime.Now;
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("index", "home");
                }

            }
            catch (Exception)
            {

                TempData["Errors"] = "Qeydiyyat olanda Problem yarandi";
                return RedirectToAction("login", "auth");

            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("id");
            return RedirectToAction("index", "home");
        }
    }

}
