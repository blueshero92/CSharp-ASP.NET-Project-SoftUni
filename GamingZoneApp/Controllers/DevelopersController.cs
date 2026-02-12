using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GamingZoneApp.Controllers
{
    public class DevelopersController : BaseController
    {

        private readonly IDeveloperService developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        //Visualize all developers using view model.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllDevelopersViewModel> developers
                = await developerService.GetAllDevelopersWithInfoAsync();

            return View(developers);
        }

        //Visualize all games by a specific developer using a view model.
        //Created buttons to be able to access this view from the Developers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeveloperGames(Guid developerId)
        {
            IEnumerable<AllGamesViewModel> gamesByDev 
                = await developerService.GetAllGamesByDeveloperIdAsync(developerId);

            return View(gamesByDev);

        }
    }
}
