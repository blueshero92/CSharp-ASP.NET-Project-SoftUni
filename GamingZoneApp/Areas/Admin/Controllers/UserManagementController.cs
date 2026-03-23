using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.User;
using Microsoft.AspNetCore.Mvc;

using static GamingZoneApp.GCommon.Constants.AppConstants;

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
            // Get all users and their roles using the user service task.
            IEnumerable<UserViewModel> allUsers = await userService.GetAllUsersAsync();

            // Get all available roles to display in the dropdown.
            ViewBag.AllRoles = await userService.GetAllRolesAsync();

            // Pass the users to the view for display.
            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(Guid userId, string selectedRole)
        {
            if (string.IsNullOrEmpty(selectedRole))
            {
                TempData[ErrorTempDataKey] = "Please select a role to assign.";
                return RedirectToAction(nameof(Index));
            }

            bool isAssigned = await userService.AssignRoleAsync(userId, selectedRole);

            if (!isAssigned)
            {
                TempData[ErrorTempDataKey] = "An error occurred while assigning the role. Please ensure the user and role exist and try again.";

                return RedirectToAction(nameof(Index));
            }

            TempData[SuccessTempDataKey] = "Role assigned successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid userId, string selectedRole)
        {
            if (string.IsNullOrEmpty(selectedRole))
            {
                TempData[ErrorTempDataKey] = "Please select a role to remove.";
                return RedirectToAction(nameof(Index));
            }

            bool isRemoved = await userService.RemoveRoleAsync(userId, selectedRole);

            if (!isRemoved)
            {
                TempData[ErrorTempDataKey] = "An error occurred while removing the role. Please ensure the user and role exist and try again.";
                return RedirectToAction(nameof(Index));
            }

            TempData[SuccessTempDataKey] = "Role removed successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
