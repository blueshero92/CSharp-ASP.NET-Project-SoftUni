using GamingZoneApp.ViewModels.Admin.User;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    }
}
