using Microsoft.AspNetCore.Mvc;

namespace BracketTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}