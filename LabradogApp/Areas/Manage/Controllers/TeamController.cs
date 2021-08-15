using LabraDog.DAL;
using LabradogApp.Dtos;
using LabradogApp.Helpers;
using LabradogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;


        public TeamController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 5m);

            List<Team> model = _context.Teams.Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(TeamCreateDto teamCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Team team = new Team();
            team.Email = teamCreateDto.Email;
            team.FullName = teamCreateDto.FullName;
            team.Info = teamCreateDto.Info;
            team.FbLink = teamCreateDto.FbLink;
            team.InstagramLink = teamCreateDto.InstagramLink;
            team.TwitterLink = teamCreateDto.TwitterLink;
            team.Phone = teamCreateDto.Phone;
            team.Job = teamCreateDto.Job;
            team.Image = FileManager.Save(teamCreateDto.Image);
            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null) return RedirectToAction("index");

            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(int id, TeamCreateDto teamCreateDto)
        {
            if (!ModelState.IsValid) return View();

            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (existTeam == null) return RedirectToAction("index");

            existTeam.Email = teamCreateDto.Email;
            existTeam.FullName = teamCreateDto.FullName;
            existTeam.Info = teamCreateDto.Info;
            existTeam.FbLink = teamCreateDto.FbLink;
            existTeam.InstagramLink = teamCreateDto.InstagramLink;
            existTeam.TwitterLink = teamCreateDto.TwitterLink;
            existTeam.Phone = teamCreateDto.Phone;
            existTeam.Job = teamCreateDto.Job;
            if (teamCreateDto.Image != null)
            {
                existTeam.Image = FileManager.Save(teamCreateDto.Image);
            }

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null) return RedirectToAction("index");

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
