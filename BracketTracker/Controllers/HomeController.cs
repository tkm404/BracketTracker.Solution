using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Linq;
using System.Collections.Generic;

namespace BracketTracker.Controllers;

public class HomeController : Controller
{
  private readonly BracketTrackerContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public HomeController(UserManager<ApplicationUser> userManager, BracketTrackerContext db)
  {
    _userManager = userManager;
    _db = db;
  }

  public ActionResult Index()
  {
    var databaseInfo = new DatabaseInfo {
      AllTeams = _db.Teams,
      Players = _db.Players
    };
    return View(databaseInfo);
  }
}

public class DatabaseInfo
{
  public IEnumerable<Team> AllTeams { get; set; }
  public IEnumerable<Team> MyTeams { get; set; }
  public IEnumerable<Player> Players { get; set; }
  public IEnumerable<PlayerTeam> PlayerTeams { get; set; }
}