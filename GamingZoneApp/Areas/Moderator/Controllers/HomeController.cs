using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Moderator.Controllers
{
    public class HomeController : BaseModeratorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
