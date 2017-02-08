using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recspecs.Models;
using Microsoft.EntityFrameworkCore;

namespace Recspecs.Controllers
{
    public class DivisionsController : Controller
    {
        private RecspecsContext db = new RecspecsContext();
        public IActionResult Index()
        {
            return View(db.Divisions.ToList());
        }

        public IActionResult Details(int id)
        {
            return View(db.Divisions.Include(divisions => divisions.Teams)
                .Where(divisions => divisions.DivisionId == id)
                .FirstOrDefault());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Division newDivision)
        {
            db.Divisions.Add(newDivision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            return View(db.Divisions.FirstOrDefault(divisions => divisions.DivisionId == id));
        }
        [HttpPost]
        public IActionResult Edit(Division Division)
        {
            db.Entry(Division).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
             var selectedDivision = (db.Divisions.FirstOrDefault(divisions => divisions.DivisionId == id));
            return View(selectedDivision);
        }

        [HttpPost, ActionName("Delete")] 
        public IActionResult DeleteConfirmed(int id)
        {
            db.Divisions.Remove(db.Divisions.FirstOrDefault(divisions => divisions.DivisionId == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
