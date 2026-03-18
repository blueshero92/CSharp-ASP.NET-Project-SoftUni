using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.Services.Models.Game;
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
            IEnumerable<DeveloperAllDto> developers
                = await developerService.GetAllDevelopersWithInfoAsync();

            IEnumerable<AllDevelopersViewModel> developersViewModel = developers
                .Select(d => new AllDevelopersViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    GamesDeveloped = d.GamesDeveloped,
                    ImageUrl = d.ImageUrl
                });

            return View(developersViewModel);
        }

        //Visualize all games by a specific developer using a view model.
        //Created buttons to be able to access this view from the Developers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeveloperGames(Guid developerId)
        {
            IEnumerable<GameAllDto> gamesByDev 
                = await developerService.GetAllGamesByDeveloperIdAsync(developerId);

            IEnumerable<AllGamesViewModel> gamesByDevViewModel = gamesByDev
                                                                .Select(g => new AllGamesViewModel
                                                                {
                                                                    Id = g.Id,
                                                                    Title = g.Title,
                                                                    ImageUrl = g.ImageUrl,
                                                                    Genre = g.Genre,
                                                                    Developer = g.Developer
                                                                });

            return View(gamesByDevViewModel);

        }
    }
}
