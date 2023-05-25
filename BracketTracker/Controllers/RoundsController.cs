using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
            ViewBag.allTeamRounds = _db.TeamRounds.ToList();
            List<Round> rounds = _db.Rounds.Include(round => round.JoinEntities)
                                            .ThenInclude(join => join.Team)
                                            .ToList();
            return View(rounds);
        }
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "Name");
            return View();
        }

        public ActionResult Play()
        {
            ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Play(Round round, int[] TeamId)
        {
            Random randy = new Random();
            if (randy.Next(2) == 0)
            {
                round.Result = false;
            }
            else
            {
                round.Result = true;
            }
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
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = false });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = true });
                _db.SaveChanges();
            }
            else
            {
                team1.Wins++;
                team2.Losses++;
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = true });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = false });
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Round thisRound = _db.Rounds

                  .Include(round => round.JoinEntities)
                  .ThenInclude(join => join.Team)
                  .FirstOrDefault(round => round.RoundId == id);
            return View(thisRound);
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
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = false });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = true });
                _db.SaveChanges();
            }
            else
            {
                team1.Wins++;
                team2.Losses++;
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = true });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = false });
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Round thisRound = _db.Rounds.FirstOrDefault(rounds => rounds.RoundId == id);
            return View(thisRound);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Round thisRound = _db.Rounds.FirstOrDefault(rounds => rounds.RoundId == id);
            _db.Rounds.Remove(thisRound);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}