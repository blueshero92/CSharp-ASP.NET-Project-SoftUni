using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Games/GameDetails/{id}")]
        public IActionResult GameDetails(int id)
        {
            string errorMessage = string.Empty;

            if(id <= 0)
            {
                errorMessage = "Invalid game Id.";                
            }
            else
            {
                errorMessage = $"Game with Id: {id}";
            }

            return Ok(errorMessage);
                
        }
    }
}
