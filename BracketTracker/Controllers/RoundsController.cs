using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace BracketTracker.Controllers
{
    public class RoundsController : Controller
    {
        private readonly BracketTrackerContext _db;
        public RoundsController(BracketTrackerContext db)
        {
            _db = db;
        }
				
				public ActionResult Index()
				{

					return View(_db.Rounds.ToList());
				}
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "Name");
            return View();
        }

				[HttpPost]
				public ActionResult Create(Round round)
				{
					_db.Rounds.Add(round);
					_db.SaveChanges();
					return RedirectToAction("Index");
				}
    }
}