using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;
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
            IEnumerable<DeveloperAllDto> developerAllDto = await developerService.GetAllDevelopersWithInfoAsync();

            IEnumerable<AllDevelopersViewModel> allDevelopersViewModel = developerAllDto
                                                                         .Select(d => new AllDevelopersViewModel
                                                                         {
                                                                             Id = d.Id,
                                                                             Name = d.Name,
                                                                             Description = d.Description,
                                                                             GamesDeveloped = d.GamesDeveloped,
                                                                             ImageUrl = d.ImageUrl
                                                                         });


            return View(allDevelopersViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddDeveloper()
        {
            DeveloperInputModel developerInputModel = new DeveloperInputModel();

            await developerManagementService.AddDeveloperAsync(developerInputModel);

            return View(developerInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeveloper(DeveloperInputModel developerInputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(developerInputModel);
            }

            bool isAdded = await developerManagementService.AddDeveloperAsync(developerInputModel);

            if (!isAdded)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the developer. Please try again.");
                return View(developerInputModel);
            }

            TempData[SuccessTempDataKey] = "Developer added successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
