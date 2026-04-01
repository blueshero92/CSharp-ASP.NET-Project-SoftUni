using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.GCommon.Pagination;
using static GamingZoneApp.GCommon.Constants.AppConstants;

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
        public async Task<IActionResult> Index(int? pageNumber)
        {
            IEnumerable<AllDevelopersViewModel> developersViewModel = await developerService.GetAllDevelopersWithInfoAsync();

            //Size of the page for pagination.
            int pageSize = PageSize;

            //Using the PaginatedList class to create a paginated list of developers and passing it to the view.
            return View(await PaginatedList<AllDevelopersViewModel>.CreateAsync(developersViewModel, pageNumber ?? 1, pageSize));
        }

        //Visualize all games by a specific developer using a view model.
        //Created buttons to be able to access this view from the Developers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeveloperGames(Guid developerId, int? pageNumber)
        {
            //Getting all games by a specific developer using the developer service and passing it to the view model.
            IEnumerable<AllGamesViewModel> gamesByDevViewModel = await developerService.GetAllGamesByDeveloperIdAsync(developerId);

            //Size of the page for pagination.
            int pageSize = PageSize;

            //Using the PaginatedList class to create a paginated list of games and passing it to the view.
            return View(await PaginatedList<AllGamesViewModel>.CreateAsync(gamesByDevViewModel, pageNumber ?? 1, pageSize));

        }
    }
}
