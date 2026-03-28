using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Publisher;
using GamingZoneApp.ViewModels.Publisher;

using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.AppConstants;
using static GamingZoneApp.GCommon.Constants.OutputMessages.PublisherManagementControllerErrors;
using static GamingZoneApp.GCommon.Constants.OutputMessages.PublisherManagementControllerSuccessMessages;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class PublisherManagementController : BaseAdminController
    {
        private readonly IPublisherService publisherService;
        private readonly IPublisherManagementService publisherManagementService;

        public PublisherManagementController(IPublisherService publisherService, IPublisherManagementService publisherManagementService)
        {
            this.publisherService = publisherService;
            this.publisherManagementService = publisherManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Map the retrieved data to a collection of AllPublishersViewModel to be used in the view.
            IEnumerable<AllPublishersViewModel> allPublishersViewModel = await publisherService.GetAllPublishersWithInfoAsync();

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
                TempData[ErrorTempDataKey] = ErrorAddingPublisher;
                return View(publisherInputModel);
            }

            // If the publisher was added successfully, set a success message in TempData and redirect to the Index action to display the list of publishers.
            TempData[SuccessTempDataKey] = PublisherAddedSuccessfullyMessage;
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
                TempData[ErrorTempDataKey] = ErrorEditingPublisher;
                return View(publisherInputModel);
            }

            //If the edit operation is successful, set a success message in TempData and redirect to the Index action to display the list of publishers.
            TempData[SuccessTempDataKey] = PublisherEditedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePublisher([FromRoute(Name = "id")] Guid publisherId)
        {
            //Check if the publisher exists using the publisherService method. If not, return a NotFound result.
            if (!await publisherService.PublisherExistsAsync(publisherId))
            {
                return NotFound();
            }

            //Use the publisherManagementService method to retrieve the publisher information for deletion based on the provided publisherId.
            DeletePublisherViewModel? deletePublisherViewModel = await publisherManagementService.GetPublisherForDeleteAsync(publisherId);

            //If the publisher information is not found, redirect to the Index action to display the list of publishers.
            if (deletePublisherViewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(deletePublisherViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePublisher([FromRoute(Name = "id")] Guid publisherId, DeletePublisherViewModel deletePublisherViewModel)
        {
            //Check if the publisher exists using the publisherService method. If not, return a NotFound result.
            if (!await publisherService.PublisherExistsAsync(publisherId))
            {
                return NotFound();
            }

            //Try to delete the publisher using the publisherManagementService method with the provided publisherId.
            bool isDeleted = await publisherManagementService.HardDeletePublisherAsync(publisherId);

            //If the delete operation fails, return the view with the current DeletePublisherViewModel and display an error message.
            if (!isDeleted)
            {
                TempData[ErrorTempDataKey] = ErrorDeletingPublisher;
                return View(deletePublisherViewModel);
            }

            //If the delete operation is successful, set a success message in TempData and redirect to the Index action to display the list of publishers.
            TempData[SuccessTempDataKey] = PublisherDeletedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
