using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using LabradogApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LabradogApp.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("admin/setting")]
        public IActionResult Setting()
        {
            Models.Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        [Route("admin/setting")]
        public IActionResult Setting(Setting setting, int id)
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
                return RedirectToAction("setting", "adminpage");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        [Route("/admin/orderlist")]
        public IActionResult OrderList()
        {
            AdminPageViewModel adminVM = new AdminPageViewModel()
            {
                Orders = _context.Orders.OrderByDescending(x => x.CreatedAt).ToList()
            };
            return View(adminVM);
        }

        [Route("/admin/contactlist")]
        public IActionResult ContactUsList()
        {
            List<ContactUs> contactUs = _context.ContactUs.OrderByDescending(x => x.Id).ToList();
            return View(contactUs);
        }

        [Route("/admin/teamlist")]
        public IActionResult TeamList()
        {
            AdminPageViewModel adminVM = new AdminPageViewModel()
            {
                Teams = _context.Teams.OrderByDescending(x => x.Id).ToList()
            };
            return View(adminVM);
        }


        [HttpGet]
        [Route("/admin/addmember")]
        public IActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        [Route("/admin/addmember")]
        public IActionResult AddTeam(TeamCreateDto team)
        {
            Team newTeam = new Team();
            newTeam.FullName = team.FullName;
            newTeam.Job = team.Info;
            newTeam.Image = FileManager.Save(team.Image);
            newTeam.Info = team.Info;
            newTeam.Email = team.Email;
            newTeam.Phone = team.Phone;
            newTeam.FbLink = team.FbLink;
            newTeam.TwitterLink = team.TwitterLink;
            newTeam.InstagramLink = team.InstagramLink;
            _context.Add(newTeam);
            _context.SaveChanges();
            return RedirectToAction("teamlist");
        }

        [Route("/admin/gallerylist")]
        public IActionResult GalleryList()
        {
            List<Image> images = _context.Images.OrderByDescending(x => x.Id).ToList();
            return View(images);
        }

        [HttpGet]
        [Route("/admin/addgalleryimage")]
        public IActionResult AddGalleryImage()
        {
            return View();
        }

        [HttpPost]
        [Route("/admin/addgalleryimage")]
        public IActionResult AddGalleryImage(GalleryImageCreateDto image)
        {
            Image newImage = new Image();
            newImage.DogName = image.DogName;
            newImage.Old = image.Old;
            newImage.DogImage = FileManager.Save(image.DogImage);
            _context.Add(newImage);
            _context.SaveChanges();
            return RedirectToAction("gallerylist");
        }

        public IActionResult DeleteGalleryImage(int id)
        {
            Image image = _context.Images.FirstOrDefault(x => x.Id == id);
            if (image != null)
            {
                FileManager.Delete(image.DogImage);
                _context.Remove(image);
                _context.SaveChanges();
            }
            return RedirectToAction("gallerylist");
        }

        [Route("/admin/categorylist")]
        public IActionResult CategoryList()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        [Route("/admin/addcategory")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("/admin/addcategory")]
        public IActionResult AddCategory(Category category)
        {
            Category newCategory = new Category();
            newCategory.Name = category.Name;
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("categorylist");
        }

        [Route("/admin/deletecategory")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("categorylist");
        }


    }
}
