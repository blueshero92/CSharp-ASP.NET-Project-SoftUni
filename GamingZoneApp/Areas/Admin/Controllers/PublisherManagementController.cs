using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.AppConstants;

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

        [HttpGet]
        public async Task<IActionResult> AddPublisher()
        {
            // Create an instance of PublisherInputModel to be used in the view for adding a new publisher.
            PublisherInputModel publisherInputModel = new PublisherInputModel();

            //Initialize the PublisherInputModel.
            await publisherManagementService.AddPublisherAsync(publisherInputModel);

            // Return the view with the PublisherInputModel to display the form for adding a new publisher.
            return View(publisherInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherInputModel publisherInputModel)
        {
            // Check if the model state is valid. If not, return the view with the current PublisherInputModel to display validation errors.
            if (!ModelState.IsValid)
            {
                return View(publisherInputModel);
            }

            // Use the publisherManagementService method to add a new publisher to the database.
            bool isAdded = await publisherManagementService.AddPublisherAsync(publisherInputModel);

            // If the publisher was not added successfully, return the view with the current PublisherInputModel to display an error message.
            if (!isAdded)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the publisher. Please try again.");
                return View(publisherInputModel);
            }

            // If the publisher was added successfully, set a success message in TempData and redirect to the Index action to display the list of publishers.
            TempData[SuccessTempDataKey] = "Publisher added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditPublisher([FromRoute(Name = "id")] Guid publisherId)
        {
            // Check if the publisher with the provided publisherId exists using the publisherService method. If not, return a NotFound result.
            if (!await publisherService.PublisherExistsAsync(publisherId))
            {
                return NotFound();
            }

            // Use the publisherManagementService method to retrieve the publisher information for editing based on the provided publisherId.
            PublisherInputModel? publisherInputModel = await publisherManagementService.GetPublisherForEditAsync(publisherId);

            // If the publisher information is not found, redirect to the Index action to display the list of publishers.
            if (publisherInputModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the PublisherInputModel to display the form for editing the publisher information.
            return View(publisherInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPublisher([FromRoute(Name = "id")] Guid publisherId, PublisherInputModel publisherInputModel)
        {
            //Check if the publisher exists using the publisherService method. If not, return a NotFound result.
            if (!await publisherService.PublisherExistsAsync(publisherId))
            {
                return NotFound();
            }

            //Try to edit the publisher information using the publisherManagementService method with the provided publisherId and PublisherInputModel. 
            bool isEdited = await publisherManagementService.EditPublisherAsync(publisherId, publisherInputModel);

            //If the edit operation fails, return the view with the current PublisherInputModel to display an error message.
            if (!isEdited)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the publisher. Please try again.");
                return View(publisherInputModel);
            }

            //If the edit operation is successful, set a success message in TempData and redirect to the Index action to display the list of publishers.
            TempData[SuccessTempDataKey] = "Publisher edited successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
