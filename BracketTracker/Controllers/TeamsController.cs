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

    public ActionResult Details(int id)
    {
      Team thisTeam = _db.Teams
            .Include(team => team.JoinEntities)
            .ThenInclude(join => join.Round)
            .FirstOrDefault(team => team.TeamId == id);
            return View(thisTeam);
    }
    public ActionResult Delete(int id)
    {
      Team thisTeam = _db.Teams.FirstOrDefault(teams => teams.TeamId == id);
      return View(thisTeam);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Team thisTeam = _db.Teams.FirstOrDefault(teams => teams.TeamId == id);
      _db.Teams.Remove(thisTeam);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}