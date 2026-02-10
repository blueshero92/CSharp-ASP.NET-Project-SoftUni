using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        //Visualize all publishers using a view model.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllPublishersViewModel> publishers 
                = await publisherService.GetAllPublishersWithInfoAsync();

            return View(publishers);
        }

        //Visualize all games by a specific publisher using a view model.
        //Created buttons to be able to access this view from the Publishers/Index view.
        [HttpGet]
        public async  Task<IActionResult> PublisherGames(Guid publisherId)
        {
            IEnumerable<AllGamesViewModel> gamesByPublisher 
                = await publisherService.GetAllGamesByPublisherIdAsync(publisherId);

            return View(gamesByPublisher);
        }
    }
}
