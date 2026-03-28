using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static GamingZoneApp.GCommon.Constants.AppConstants;
using static GamingZoneApp.GCommon.Constants.OutputMessages.UserManagementControllerErrors;
using static GamingZoneApp.GCommon.Constants.OutputMessages.UserManagementControllerSuccessMessages;

namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            // Get the current admin's ID from the claims to exclude them from the user list.
            // Admin should not be able to modify their own roles to prevent accidental lockout.
            // Also, there will be only one Admin user in the system.
            Guid currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Get all users and their roles using the user service task.
            //Exclude the current admin from the list to prevent self-modification of roles.
            IEnumerable<UserViewModel> allUsers = (await userService.GetAllUsersAsync())
                                                   .Where(u => u.Id != currentAdminId)
                                                   .ToList();

            // Get all available roles to display in the dropdown.
            ViewBag.AllRoles = await userService.GetAllRolesAsync();

            // Pass the users to the view for display.
            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(Guid userId, string selectedRole)
        {
            // Validate that a role was selected before attempting to assign it to the user.
            if (string.IsNullOrEmpty(selectedRole))
            {
                TempData[ErrorTempDataKey] = ErrorRoleNotSelected;
                return RedirectToAction(nameof(Index));
            }

            // Call the AssignRoleAsync method of the user service to assign the selected role to the specified user.
            bool isAssigned = await userService.AssignRoleAsync(userId, selectedRole);

            // If the role assignment was not successful, set an error message in TempData and redirect back to the Index action.
            if (!isAssigned)
            {
                TempData[ErrorTempDataKey] = ErrorAssigningRole;

                return RedirectToAction(nameof(Index));
            }

            // If the role assignment was successful, set a success message in TempData and redirect back to the Index action.
            TempData[SuccessTempDataKey] = RoleAssignedSuccessfullyMessage;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid userId, string selectedRole)
        {
            // Validate that a role was selected before attempting to remove it from the user.
            if (string.IsNullOrEmpty(selectedRole))
            {
                TempData[ErrorTempDataKey] = ErrorRoleNotSelectedForRemoval;
                return RedirectToAction(nameof(Index));
            }

            // Call the RemoveRoleAsync method of the user service to remove the selected role from the specified user.
            bool isRemoved = await userService.RemoveRoleAsync(userId, selectedRole);

            // If the role removal was not successful, set an error message in TempData and redirect back to the Index action.
            if (!isRemoved)
            {
                TempData[ErrorTempDataKey] = ErrorRemovingRole;
                return RedirectToAction(nameof(Index));
            }

            // If the role removal was successful, set a success message in TempData and redirect back to the Index action.
            TempData[SuccessTempDataKey] = RoleRemovedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            // Call the GetUserForDeletionAsync method of the user service to retrieve the user details for confirmation before deletion.
            DeleteUserViewModel? userForDeletion = await userService.GetUserForDeletionAsync(userId);

            // If the user was not found, set an error message in TempData and redirect back to the Index action.
            if (userForDeletion == null)
            {
                TempData[ErrorTempDataKey] = UserNotFoundError;
                return RedirectToAction(nameof(Index));
            }

            // Pass the user details to the view for confirmation before deletion.
            return View(userForDeletion);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid userId, DeleteUserViewModel model)
        {
            // Call the DeleteUserAsync method of the user service to delete the specified user from the system.
            bool isDeleted = await userService.DeleteUserAsync(userId);

            // If the user deletion was not successful, set an error message in TempData and redirect back to the Index action.
            if (!isDeleted)
            {
                TempData[ErrorTempDataKey] = ErrorDeletingUser;
                return RedirectToAction(nameof(Index));
            }

            // If the user deletion was successful, set a success message in TempData and redirect back to the Index action.
            TempData[SuccessTempDataKey] = UserDeletedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
