using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperService
    {
        Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync();

        Task<bool> DeveloperExistsAsync(Guid developerId);

    }
}
