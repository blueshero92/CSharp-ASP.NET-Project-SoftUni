using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Controllers
{
    public class PublishersController : BaseController
    {
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        //Visualize all publishers using a view model.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<PublisherAllDto> publishers 
                = await publisherService.GetAllPublishersWithInfoAsync();

            IEnumerable<AllPublishersViewModel> publishersViewModel = publishers
                .Select(p => new AllPublishersViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    GamesPublished = p.GamesPublished,
                    ImageUrl = p.ImageUrl
                });

            return View(publishersViewModel);
        }

        //Visualize all games by a specific publisher using a view model.
        //Created buttons to be able to access this view from the Publishers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async  Task<IActionResult> PublisherGames(Guid publisherId)
        {
            IEnumerable<GameAllDto> gamesByPublisher 
                = await publisherService.GetAllGamesByPublisherIdAsync(publisherId);

            IEnumerable<AllGamesViewModel> gamesByPublisherViewModel = gamesByPublisher
                .Select(g => new AllGamesViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre,
                    Developer = g.Developer,
                    ImageUrl = g.ImageUrl
                });

            return View(gamesByPublisherViewModel);
        }
    }
}
