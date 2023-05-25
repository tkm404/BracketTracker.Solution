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
            ViewBag.winnerBracket = _db.Winners.ToList();
            ViewBag.loserBracket = _db.Losers.ToList();
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
                            if (team1.Losses >= 2)
            {
                Loser loser = _db.Losers.FirstOrDefault(lsr => lsr.TeamId == team1.TeamId);
                _db.Losers.Remove(loser);
            }
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);
                _db.SaveChanges();
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = false });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = true });
                _db.SaveChanges();
#nullable enable
                Winner? bracketLwr = _db.Winners.FirstOrDefault(win => win.TeamId == team1.TeamId);
#nullable disable
                if (bracketLwr == null && team1.Losses == 1)
                {
                    Loser loser = (new Loser() { TeamId = team1.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Winners.Remove(bracketLwr);
                    Loser loser = (new Loser() { TeamId = team1.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
#nullable enable
                Winner? actualWinner = _db.Winners.FirstOrDefault(win => win.TeamId == team1.TeamId);
#nullable disable
                if (actualWinner == null)
                {
                    Winner roundWinner = (new Winner() { TeamId = team2.TeamId });
                    _db.Winners.Add(roundWinner);
                    _db.SaveChanges();
                }
            }
            else
            {
                team1.Wins++;
                team2.Losses++;
                            if (team2.Losses >= 2)
            {
                Loser loser = _db.Losers.FirstOrDefault(lsr => lsr.TeamId == team2.TeamId);
                _db.Losers.Remove(loser);
            }
            _db.SaveChanges();
                _db.Teams.Update(team1);
                _db.Teams.Update(team2);

                _db.SaveChanges();
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[0], Outcome = true });
                _db.TeamRounds.Add(new TeamRound() { RoundId = round.RoundId, TeamId = TeamId[1], Outcome = false });
                _db.SaveChanges();
#nullable enable
                Winner? bracketLwr = _db.Winners.FirstOrDefault(win => win.TeamId == team2.TeamId);
#nullable disable
                if (bracketLwr == null && team2.Losses == 1)
                {
                    Loser loser = (new Loser() { TeamId = team2.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
                else
                {
// #nullable enable
//                 Winner? bracketLwr = _db.Winners.FirstOrDefault(win => win.TeamId == team2.TeamId);
// #nullable disable
                    _db.Winners.Remove(bracketLwr);
                    Loser loser = (new Loser() { TeamId = team2.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
#nullable enable
                Winner? actualWinner = _db.Winners.FirstOrDefault(win => win.TeamId == team1.TeamId);
#nullable disable
                if (actualWinner == null)
                {
                    Winner roundWinner = (new Winner() { TeamId = team1.TeamId });
                    _db.Winners.Add(roundWinner);
                    _db.SaveChanges();
                }

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
                Winner winner = (new Winner() { TeamId = team2.TeamId });
                _db.Winners.Add(winner);
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
                Winner winner = (new Winner() { TeamId = team1.TeamId });
                _db.Winners.Add(winner);
                _db.SaveChanges();
            }
            if (team1.Losses >= 2)
            {
#nullable enable
                Winner? winner = _db.Winners.FirstOrDefault(win => win.TeamId == team1.TeamId);
#nullable disable
                if (winner == null)
                {
                    Loser loser = (new Loser() { TeamId = team1.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
            }
            if (team2.Losses >= 2)
            {
#nullable enable
                Winner? winner = _db.Winners.FirstOrDefault(win => win.TeamId == team2.TeamId);
#nullable disable
                if (winner == null)
                {
                    Loser loser = (new Loser() { TeamId = team2.TeamId });
                    _db.Losers.Add(loser);
                    _db.SaveChanges();
                }
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