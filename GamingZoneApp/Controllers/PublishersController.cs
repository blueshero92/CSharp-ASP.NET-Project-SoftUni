using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    public class PublishersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
