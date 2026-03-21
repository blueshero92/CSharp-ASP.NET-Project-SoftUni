using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class GameManagementController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IGameManagementService gameManagementService;
        private readonly IDeveloperService developerService;
        private readonly IPublisherService publisherService;

        //gameService, developerService and publisherService are reused for getting the necessary data to display in the views.
        public GameManagementController(IGameService gameService, IGameManagementService gameManagementService,
                                       IDeveloperService developerService, IPublisherService publisherService)
        {
            this.gameService = gameService;
            this.gameManagementService = gameManagementService;
            this.developerService = developerService;
            this.publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GameAllDto> games = await gameService.GetAllGamesAsync();

            IEnumerable<AllGamesViewModel> allGames = games
                .Select(g => new AllGamesViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl ?? null,
                    Genre = g.Genre,
                    Developer = g.Developer
                });

            return View(allGames);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Using the service method to get the game details for editing.
            GameInputModel? gameInputModel = await gameManagementService.GetEditAsync(id);

            //Using helper method from the game service(for reusability) to check if the game exists.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //If the game details are not found, redirect to the edit page.
            if (gameInputModel == null)
            {
                return RedirectToAction(nameof(Edit));
            }

            //Populating the developers and publishers collections for the dropdown lists in the edit view.
            gameInputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
            gameInputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

            return View(gameInputModel);
        }

    }
}
