using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BracketTracker.Controllers;

public class TeamsController : Controller
{
  private readonly BracketTrackerContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public TeamsController(UserManager<ApplicationUser> userManager, BracketTrackerContext db)
  {
    _userManager = userManager;
    _db = db;
  }


  public async Task<ActionResult> Index()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    var teams = new DatabaseInfo {
      AllTeams = _db.Teams
                  .Include(player => player.Players)
                  .Include(teamround => teamround.JoinEntities)
                  .ThenInclude(round => round.Round),
    };
    if (currentUser != null)
    {
      teams.MyTeams = _db.Teams
                            .Where(entry => entry.User.Id == currentUser.Id)
                            .Include(player => player.Players)
                            .Include(teamround => teamround.JoinEntities)
                            .ThenInclude(round => round.Round);
    }
    return View(teams);
  }

  [Authorize]
  public ActionResult Create()
  {
    return View();
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult> Create(Team team)
  {
    if (!ModelState.IsValid)
    {
      return View(team);
    }
    else
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      team.User = currentUser;
      _db.Teams.Add(team);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }



  public ActionResult Details(int id)
  {
    var thisTeam = _db.Teams
          .Include(team => team.JoinEntities)
          .ThenInclude(join => join.Round)
          .Include(playerTeam => playerTeam.PlayerTeams)
          .ThenInclude(player => player.Player)
          .FirstOrDefault(team => team.TeamId == id);
    return View(thisTeam);
  }

  public async Task<ActionResult> MyTeam()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    var userTeams = new DatabaseInfo {
      MyTeams = _db.Teams
                    .Where(e =>e.User.Id == currentUser.Id)
                    .Include(player => player.Players)
                    .Include(teamround => teamround.JoinEntities)
                    .ThenInclude(round => round.Round)
    };
    return View(userTeams);
  }

  [Authorize]
  public ActionResult Delete(int id)
  {
    Team thisTeam = _db.Teams.FirstOrDefault(teams => teams.TeamId == id);
    return View(thisTeam);
  }

  [Authorize]
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Team thisTeam = _db.Teams.FirstOrDefault(teams => teams.TeamId == id);
    _db.Teams.Remove(thisTeam);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}