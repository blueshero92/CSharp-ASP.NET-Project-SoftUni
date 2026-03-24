using GamingZoneApp.ViewModels.Admin.User;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<IEnumerable<string>> GetAllRolesAsync();

        Task<bool> AssignRoleAsync(Guid userId, string roleName);

        Task<bool> RemoveRoleAsync(Guid userId, string roleName);

        Task<DeleteUserViewModel?> GetUserForDeletionAsync(Guid userId);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
