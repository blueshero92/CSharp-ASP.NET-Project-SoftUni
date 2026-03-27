using GamingZoneApp.Services.Core.Interfaces;
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
            //Get all publishers with their info using the service and map them to the view model.
            IEnumerable<AllPublishersViewModel> publishersViewModel = await publisherService.GetAllPublishersWithInfoAsync();
                
            return View(publishersViewModel);
        }

        //Visualize all games by a specific publisher using a view model.
        //Created buttons to be able to access this view from the Publishers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async  Task<IActionResult> PublisherGames(Guid publisherId)
        {
            //Get all games by the publisher's id using the service and map them to the view model.
            IEnumerable<AllGamesViewModel> gamesByPublisherViewModel = await publisherService.GetAllGamesByPublisherIdAsync(publisherId);

            return View(gamesByPublisherViewModel);
        }
    }
}
