using LabraDog.DAL;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminPageController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/setting")]
        public IActionResult Setting()
        {
            Models.Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        [Route("admin/setting")]
        [Authorize(Roles = "Admin")]
        public IActionResult Setting(Setting setting,int id)
        {
            try
            {
                var oldSetting = _context.Settings.Where(s => s.Id == id).SingleOrDefault();
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
                _context.SaveChanges();
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
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            User user = await _userManager.FindByNameAsync(loginView.UserName);

            if (user == null || user.isAdmin != true)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect or you don't admin");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginView.Password, loginView.IsPersistent, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }

            return RedirectToAction("index");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
