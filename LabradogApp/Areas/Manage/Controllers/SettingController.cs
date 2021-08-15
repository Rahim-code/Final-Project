using LabraDog.DAL;
using LabradogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LabradogApp.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SettingController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("/manage/setting")]
        public IActionResult Setting()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        [Route("/manage/setting")]
        public IActionResult Setting(Setting setting, int id)
        {
            try
            {
                Setting oldSetting = _context.Settings.Where(s => s.Id == id).SingleOrDefault();
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
                return RedirectToAction("setting");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
