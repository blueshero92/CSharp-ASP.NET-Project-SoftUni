using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;

using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.AppConstants;
using static GamingZoneApp.GCommon.Constants.OutputMessages.DeveloperManagementControllerErrors;
using static GamingZoneApp.GCommon.Constants.OutputMessages.DeveloperManagemntControllerSuccessMessages;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class DeveloperManagementController : BaseAdminController
    {
        private readonly IDeveloperService developerService;
        private readonly IDeveloperManagementService developerManagementService;

        public DeveloperManagementController(IDeveloperService developerService, IDeveloperManagementService developerManagementService)
        {
            this.developerService = developerService;
            this.developerManagementService = developerManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Map the retrieved data to a collection of AllDevelopersViewModel to be used in the view.
            IEnumerable<AllDevelopersViewModel> allDevelopersViewModel = await developerService.GetAllDevelopersWithInfoAsync();

            return View(allDevelopersViewModel);
        }

        [HttpGet]
        public IActionResult AddDeveloper()
        {
            // Create an instance of DeveloperInputModel to be used in the view for adding a new developer.
            DeveloperInputModel developerInputModel = new DeveloperInputModel();

            // Return the view with the DeveloperInputModel to display the form for adding a new developer.
            return View(developerInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeveloper(DeveloperInputModel developerInputModel)
        {
            // Check if the model state is valid. If not, return the view with the current DeveloperInputModel to display validation errors.
            if (!ModelState.IsValid)
            {
                return View(developerInputModel);
            }

            // Call the AddDeveloperAsync method of the developerManagementService to add the new developer using the provided DeveloperInputModel.
            bool isAdded = await developerManagementService.AddDeveloperAsync(developerInputModel);

            // If the developer was not added successfully, add a model error and return the view with the current DeveloperInputModel to display the error message.
            if (!isAdded)
            {
                TempData[ErrorTempDataKey] = ErrorAddingDeveloper;
                return View(developerInputModel);
            }

            // If the developer was added successfully, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = DeveloperAddedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditDeveloper([FromRoute(Name = "id")] Guid developerId)
        {
            // Check if the developer with the specified ID exists using the developerService. If not, return a NotFound result.
            if (!await developerService.DeveloperExistsAsync(developerId))
            {
                return NotFound();
            }

            // Retrieve the developer information for editing using the developerManagementService.
            DeveloperInputModel? developerInputModel = await developerManagementService.GetDeveloperForEditAsync(developerId);

            // If the developer information is not found, redirect to the Index action.
            if (developerInputModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the DeveloperInputModel to display the form for editing the developer.
            return View(developerInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeveloper([FromRoute(Name = "id")] Guid developerId, DeveloperInputModel developerInputModel)
        {
            // Check if the developer with the specified ID exists using the developerService. If not, return a NotFound result.
            if (!await developerService.DeveloperExistsAsync(developerId))
            {
                return NotFound();
            }

            // Check if the model state is valid. If not, add an error and return the view with the current DeveloperInputModel to display validation errors.
            if (!ModelState.IsValid)
            {
                TempData[ErrorTempDataKey] = ErrorEditingDeveloperForm;
                return View(developerInputModel);
            }

            // Call the EditDeveloperAsync method of the developerManagementService to edit the developer information using the provided DeveloperInputModel and developer ID.
            bool editSuccessful = await developerManagementService.EditDeveloperAsync(developerId, developerInputModel);

            // If the edit operation was not successful, add an error and return the view with the current DeveloperInputModel to display the error message.
            if (!editSuccessful)
            {
                TempData[ErrorTempDataKey] = ErrorEditingDeveloper;
                return View(developerInputModel);
            }

            // If the edit operation was successful, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = DeveloperEditedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> DeleteDeveloper([FromRoute(Name = "id")] Guid developerId)
        {
            // Check if the developer with the specified ID exists using the developerService. If not, return a NotFound result.
            if (!await developerService.DeveloperExistsAsync(developerId))
            {
                return NotFound();
            }

            // Retrieve the developer information for deletion using the developerManagementService.
            DeleteDeveloperViewModel? deleteDeveloperViewModel = await developerManagementService.GetDeveloperForDeleteAsync(developerId);

            // If the developer information is not found, redirect to the Index action.
            if (deleteDeveloperViewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the DeleteDeveloperViewModel to display the confirmation page for deleting the developer.
            return View(deleteDeveloperViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeveloper([FromRoute(Name = "id")] Guid developerId, DeleteDeveloperViewModel deleteDeveloperViewModel)
        {
            // Check if the developer with the specified ID exists using the developerService. If not, return a NotFound result.
            if (!await developerService.DeveloperExistsAsync(developerId))
            {
                return NotFound();
            }

            // Call the DeleteDeveloperAsync method of the developerManagementService to delete the developer using the provided developer ID.
            bool deleteSuccessful = await developerManagementService.HardDeleteDeveloperAsync(developerId);

            // If the delete operation was not successful, add an error and return the view with the current DeleteDeveloperViewModel to display the error message.
            if (!deleteSuccessful)
            {
                TempData[ErrorTempDataKey] = ErrorDeletingDeveloper;
                return View(deleteDeveloperViewModel);
            }

            // If the delete operation was successful, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = DeveloperDeletedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
