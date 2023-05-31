using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BracketTracker.Controllers
{
  public class PlayersController : Controller
  {
    private readonly BracketTrackerContext _db;
    public PlayersController(BracketTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Players.ToList());
    }

    public ActionResult Details(int id)
    {
      Player thisPlayer = _db.Players
                              .Include(pt => pt.PlayerTeams)
                              .ThenInclude(team => team.Team)
                              .FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    // all below to be required by Admin Authorization
    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Player player)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      else
      {
        _db.Players.Add(player);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Player thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Player thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      _db.Players.Remove(thisPlayer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Player thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Player player)
    {
      _db.Players.Update(player);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AssignPlayer(int teamId)
    {
      ViewBag.PlayersList = _db.Players.ToList();
      ViewBag.TeamId = teamId;
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult AssignPlayer(Player player, int teamId)
    {
      #nullable enable
      PlayerTeam? joinEntity = _db.PlayerTeams.FirstOrDefault(join => (join.TeamId == teamId && join.PlayerId == player.PlayerId));
      #nullable disable
      if (joinEntity == null && teamId !=0)
      {
        _db.PlayerTeams.Add(new PlayerTeam() { TeamId = teamId, PlayerId = player.PlayerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", "Teams", new { id = teamId });
    }
  }
}