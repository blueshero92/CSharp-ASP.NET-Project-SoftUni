using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.User;
using Microsoft.AspNetCore.Mvc;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserViewModel> allUsers = await userService.GetAllUsersAsync();

            return View(allUsers);
        }
    }
}
