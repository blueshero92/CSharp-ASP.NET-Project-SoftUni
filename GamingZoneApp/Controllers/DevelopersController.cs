using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    public class DevelopersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
