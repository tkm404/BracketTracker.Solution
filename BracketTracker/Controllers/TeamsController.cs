using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace BracketTracker.Controllers
{
    public class TeamsController : Controller
{
    private readonly BracketTrackerContext _db;
    public TeamsController(BracketTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
        return View(_db.Teams.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
		public ActionResult Create(Team team)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      else
      {
      _db.Teams.Add(team);
      _db.SaveChanges();
			return RedirectToAction("Index");
      }
    }
  }
}