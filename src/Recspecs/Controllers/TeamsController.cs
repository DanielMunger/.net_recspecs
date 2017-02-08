using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recspecs.Models;
using Microsoft.EntityFrameworkCore;

namespace Recspecs.Controllers
{
    public class TeamsController : Controller
    {
        private RecspecsContext db = new RecspecsContext();   
        public IActionResult Index()
        {
            return View(db.Teams.Include(teams => teams.Division).ToList());
        }

        public IActionResult Details(int id)
        {
            return View(db.Teams.Include(teams => teams.Division)
                       .Include(teams => teams.Players)
                       .FirstOrDefault(teams => teams.TeamId == id)
                       );
        }
        
        public IActionResult Create()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            team.Losses = 0;
            team.Wins = 0;
            db.Teams.Add(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            return View(db.Teams.FirstOrDefault(teams => teams.TeamId == id));
        }

        [HttpPost]
        public IActionResult Edit(Team team)
        {
            db.Entry(team).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(db.Teams.FirstOrDefault(teams => teams.TeamId == id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            db.Teams.Remove(db.Teams.FirstOrDefault(teams => teams.TeamId == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
