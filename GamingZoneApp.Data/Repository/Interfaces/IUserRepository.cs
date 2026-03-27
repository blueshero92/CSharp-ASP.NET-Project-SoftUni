using GamingZoneApp.Services.Models.User;

namespace GamingZoneApp.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserAllDto>> GetAllUsersWithTheirRolesAsync();

        Task<IEnumerable<string?>> GetAllRolesByNameAsync();

        Task<bool> AssignRoleToUserAsync(Guid userId, string roleName);

        Task<bool> RemoveRoleFromUserAsync(Guid userId, string roleName);

        Task<DeleteUserDto> GetDeleteUserAsync(Guid userId);

        Task<bool> PostDeleteUserAsync(Guid userId);
    }
}
