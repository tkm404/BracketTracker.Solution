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
					List<Round> rounds = _db.Rounds.Include(round => round.JoinEntities)
																				.ThenInclude(join => join.Team).ToList();
					return View(rounds);
				}
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "Name");
            return View();
        }

				[HttpPost]
				public ActionResult Create(Round round, int[] TeamId)
				{
					_db.Rounds.Add(round);
					_db.SaveChanges();
					_db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0]});
					_db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1]});
					_db.SaveChanges();
					return RedirectToAction("Index");
				}
    }
}