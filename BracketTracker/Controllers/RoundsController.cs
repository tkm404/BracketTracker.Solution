using Microsoft.AspNetCore.Mvc;

namespace BracketTracker.Controllers
{
    public class RoundsController : Controller
{

    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }

}
}