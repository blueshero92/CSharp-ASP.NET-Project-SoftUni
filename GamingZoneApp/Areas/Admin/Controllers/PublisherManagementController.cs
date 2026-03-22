using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class PublisherManagementController : BaseController
    {
        private readonly IPublisherService publisherService;
        private readonly IPublisherManagementService publisherManagementService;

        public PublisherManagementController(IPublisherService publisherService, IPublisherManagementService publisherManagementService)
        {
            this.publisherService = publisherService;
            this.publisherManagementService = publisherManagementService;
        }
        public async Task<IActionResult> Index()
        {
            // Get all publishers with their information using the publisherService method.
            IEnumerable<PublisherAllDto> publisherAllDto = await publisherService.GetAllPublishersWithInfoAsync();

            // Map the retrieved data to a collection of AllPublishersViewModel to be used in the view.
            IEnumerable<AllPublishersViewModel> allPublishersViewModel = publisherAllDto
                                                                         .Select(d => new AllPublishersViewModel
                                                                         {
                                                                             Id = d.Id,
                                                                             Name = d.Name,
                                                                             Description = d.Description,
                                                                             GamesPublished = d.GamesPublished,
                                                                             ImageUrl = d.ImageUrl
                                                                         });


            return View(allPublishersViewModel);
        }
    }
}
