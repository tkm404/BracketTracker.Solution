using Microsoft.AspNetCore.Mvc;

namespace BracketTracker.Controllers
{
    public class TeamsController : Controller
{

    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }

}
}