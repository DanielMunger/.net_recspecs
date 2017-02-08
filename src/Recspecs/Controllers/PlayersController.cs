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
    public class PlayersController : Controller
    {
        private RecspecsContext db = new RecspecsContext();
        public IActionResult Index()
        {
            return View(db.Players.ToList());
        }
        
        public IActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName");
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Player player)
        {
            db.Players.Add(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.Players.Include(players => players.Team)
                .Where(players => players.PlayerId == id)
                .FirstOrDefault());
        }
        
    }
}
