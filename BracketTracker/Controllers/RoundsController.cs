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
            List<TeamRound> teamRounds = _db.TeamRounds.Include(teamRound => teamRound.Team)
                                                                        .Include(teamRounds => teamRounds.Round)
                                                                        .ToList();
            return View(teamRounds);
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
            Team team1 = _db.Teams.FirstOrDefault(team => team.TeamId == TeamId[0]);
            Team team2 = _db.Teams.FirstOrDefault(team => team.TeamId == TeamId[1]);
            if (round.Result == false)
            {
                team1.Losses++;
                team2.Wins++;
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = false});
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = true});
                _db.SaveChanges();
            }
            else
            {
                team1.Wins++;
                team2.Losses++;
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);
								_db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = true});
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = false});
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}