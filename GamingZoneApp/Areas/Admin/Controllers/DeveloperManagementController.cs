using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;

using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.AppConstants;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class DeveloperManagementController : BaseController
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
        public async Task<IActionResult> AddDeveloper()
        {
            // Create an instance of DeveloperInputModel to be used in the view for adding a new developer.
            DeveloperInputModel developerInputModel = new DeveloperInputModel();

            //Initialize the DeveloperInputModel.
            await developerManagementService.AddDeveloperAsync(developerInputModel);

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
                ModelState.AddModelError(string.Empty, "An error occurred while adding the developer. Please try again.");
                return View(developerInputModel);
            }

            // If the developer was added successfully, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = "Developer added successfully!";
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

            // Check if the model state is valid. If not, add a model error and return the view with the current DeveloperInputModel to display validation errors.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors in the form.");
                return View(developerInputModel);
            }

            // Call the EditDeveloperAsync method of the developerManagementService to edit the developer information using the provided DeveloperInputModel and developer ID.
            bool editSuccessful = await developerManagementService.EditDeveloperAsync(developerId, developerInputModel);

            // If the edit operation was not successful, add a model error and return the view with the current DeveloperInputModel to display the error message.
            if (!editSuccessful)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the developer. Please try again.");
                return View(developerInputModel);
            }

            // If the edit operation was successful, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = "Developer edited successfully!";
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

            // If the delete operation was not successful, add a model error and return the view with the current DeleteDeveloperViewModel to display the error message.
            if (!deleteSuccessful)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the developer. Please try again.");
                return View(deleteDeveloperViewModel);
            }

            // If the delete operation was successful, set a success message in TempData and redirect to the Index action to display the list of developers.
            TempData[SuccessTempDataKey] = "Developer deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
