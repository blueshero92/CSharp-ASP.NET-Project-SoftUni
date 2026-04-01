using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;
using GamingZoneApp.GCommon.Pagination;

using static GamingZoneApp.GCommon.Constants.AppConstants;

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
        public async Task<IActionResult> Index(int? pageNumber)
        {
            //Get all publishers with their info using the service and map them to the view model.
            IEnumerable<AllPublishersViewModel> publishersViewModel = await publisherService.GetAllPublishersWithInfoAsync();

            //Size of the page for pagination.
            int pageSize = PageSize;

            //Return the view with the paginated list of publishers.
            return View(await PaginatedList<AllPublishersViewModel>.CreateAsync(publishersViewModel, pageNumber ?? 1, pageSize));
        }

        //Visualize all games by a specific publisher using a view model.
        //Created buttons to be able to access this view from the Publishers/Index view.
        [HttpGet]
        [AllowAnonymous]
        public async  Task<IActionResult> PublisherGames(Guid publisherId, int? pageNumber)
        {
            //Get all games by the publisher's id using the service and map them to the view model.
            IEnumerable<AllGamesViewModel> gamesByPublisherViewModel = await publisherService.GetAllGamesByPublisherIdAsync(publisherId);

            //Size of the page for pagination.
            int pageSize = PageSize;

            //Return the view with the paginated list of games by the publisher.
            return View(await PaginatedList<AllGamesViewModel>.CreateAsync(gamesByPublisherViewModel, pageNumber ?? 1, pageSize));
        }
    }
}
