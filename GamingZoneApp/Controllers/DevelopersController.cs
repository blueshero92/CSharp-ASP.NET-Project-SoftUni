using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GamingZoneApp.Controllers
{
    public class DevelopersController : Controller
    {

        private readonly IDeveloperService developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        //Visualize all developers using view model.

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllDevelopersViewModel> developers
                = await developerService.GetAllDevelopersWithInfoAsync();

            return View(developers);
        }

        //Visualize all games by a specific developer using a view model.
        //Created buttons to be able to access this view from the Developers/Index view.

        [HttpGet]
        public async Task<IActionResult> DeveloperGames(Guid developerId)
        {
            IEnumerable<AllGamesViewModel> gamesByDev 
                = await developerService.GetAllGamesByDeveloperIdAsync(developerId);

            return View(gamesByDev);

        }
    }
}
