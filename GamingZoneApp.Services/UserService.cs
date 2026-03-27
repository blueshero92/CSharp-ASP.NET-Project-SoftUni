using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.User;
using GamingZoneApp.ViewModels.Admin.User;


namespace GamingZoneApp.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            // Retrieve all users along with their roles from the repository.
            IEnumerable<UserAllDto> userAllDto = await userRepository.GetAllUsersWithTheirRolesAsync();

            //Map each user to a UserViewModel, including their roles.
            IEnumerable<UserViewModel> usersVm = userAllDto.Select(u => new UserViewModel
            {
               Id = u.Id,
               Username = u.Username,
               Email = u.Email,
               Roles = u.Roles
            })
           .ToList();


            return usersVm;

        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            // Retrieve all role names from the repository.
            IEnumerable<string?> roles = await userRepository.GetAllRolesByNameAsync();

            return roles!;
        }

        public async Task<bool> AssignRoleAsync(Guid userId, string roleName)
        {
            // Assign the specified role to the user using the repository method.
            bool isUserAssignedToRole = await userRepository.AssignRoleToUserAsync(userId, roleName);

            // If the role assignment was unsuccessful, return false to indicate that the operation failed.
            if (!isUserAssignedToRole)
            {
                return false;
            }

            // If the role assignment was successful, return true to indicate that the operation succeeded.
            return isUserAssignedToRole;
        }

        public async Task<bool> RemoveRoleAsync(Guid userId, string roleName)
        {
            // Remove the specified role from the user using the repository method.
            bool isUserRemovedFromRole = await userRepository.RemoveRoleFromUserAsync(userId, roleName);

            // If the role removal was unsuccessful, return false to indicate that the operation failed.
            if (!isUserRemovedFromRole)
            {
                return false;
            }

            // If the role removal was successful, return true to indicate that the operation succeeded.
            return isUserRemovedFromRole;
        }

        public async Task<DeleteUserViewModel?> GetUserForDeletionAsync(Guid userId)
        {
            // Retrieve the user details for deletion using the repository method.
            DeleteUserDto? userToDelete = await userRepository.GetDeleteUserAsync(userId);

            //Map the retrieved user details to a DeleteUserViewModel, which will be used in the view for confirmation before deletion.
            DeleteUserViewModel userForDeletionVm = new DeleteUserViewModel
            {
                Id = userToDelete.Id,
                Username = userToDelete.Username!,
                Email = userToDelete.Email!
            };

            // Return the DeleteUserViewModel containing the user details for deletion.
            return userForDeletionVm;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            // Delete the user using the repository method.
            bool isUserDeleted = await userRepository.PostDeleteUserAsync(userId);

            // If the user deletion was unsuccessful, return false to indicate that the operation failed.
            if (!isUserDeleted)
            {
                return false;
            }

            // If the user deletion was successful, return true to indicate that the operation succeeded.
            return isUserDeleted;
        }

    }
}
