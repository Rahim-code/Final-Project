using LabraDog.Models.EntityFramework;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LabradogApp.Controllers
{
    public class AdminPageController : Controller
    {
        private EfContext context = new EfContext();
        
        [Route("/admin")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("admin/setting")]
        public IActionResult Setting()
        {
            Models.Setting setting = context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        [Route("admin/setting")]
        [Authorize]
        public IActionResult Setting(Setting setting,int id)
        {
            try
            {
                var oldSetting = context.Settings.Where(s => s.Id == id).SingleOrDefault();
                oldSetting.Phone = setting.Phone;
                oldSetting.Email = setting.Email;
                oldSetting.LifeExp = setting.LifeExp;
                oldSetting.History = setting.History;
                oldSetting.Height = setting.Height;
                oldSetting.Weight = setting.Weight;
                oldSetting.YoutubeLink = setting.YoutubeLink;
                oldSetting.WhyBestDesc = setting.WhyBestDesc;
                oldSetting.TrainAbility = setting.TrainAbility;
                oldSetting.DogCare = setting.DogCare;
                oldSetting.Adress = setting.Adress;
                oldSetting.EnergyLevel = setting.EnergyLevel;
                oldSetting.RelationChild = setting.RelationChild;
                oldSetting.LabradorDesc = setting.LabradorDesc;
                oldSetting.History = setting.History;
                oldSetting.Fact = setting.Fact;
                oldSetting.Temprament = setting.Temprament;
                context.SaveChanges();
                return RedirectToAction("setting","adminpage");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            var datas = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Status == false);
            if (datas!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,datas.Name),
                    new Claim(ClaimTypes.Email,datas.Email),
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("index", "adminPage");
            }
            TempData["error"] = "Email və ya şifrə səhvdir";
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "adminPage");
        }
    }
}
