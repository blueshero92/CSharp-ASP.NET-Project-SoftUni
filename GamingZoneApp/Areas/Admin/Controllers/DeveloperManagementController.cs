using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.ViewModels.Developer;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class DeveloperManagementController : BaseController
    {
        private readonly IDeveloperService developerService;

        public DeveloperManagementController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<DeveloperAllDto> developerAllDto = await developerService.GetAllDevelopersWithInfoAsync();

            IEnumerable<AllDevelopersViewModel> allDevelopersViewModel =  developerAllDto
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
    }
}
