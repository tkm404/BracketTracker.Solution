using Microsoft.AspNetCore.Mvc;
using BracketTracker.Models;
using System.Linq;
using System.Collections.Generic;

namespace BracketTracker.Controllers;

public class HomeController : Controller
{
  private readonly BracketTrackerContext _db;

  public HomeController(BracketTrackerContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    return View();
  }
}

public class DatabaseInfo
{
  public IEnumerable<Team> Teams { get; set; }
  public IEnumerable<Player> Players { get; set; }
}