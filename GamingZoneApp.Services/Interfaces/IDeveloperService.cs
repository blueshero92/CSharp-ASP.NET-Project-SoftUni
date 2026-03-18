using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperService
    {
        //Task for viewing all developers with their info.
        Task<IEnumerable<DeveloperAllDto>> GetAllDevelopersWithInfoAsync();

        //Task for viewing all games by a specific developer.
        Task<IEnumerable<GameAllDto>> GetAllGamesByDeveloperIdAsync(Guid developerId);

        //Helper method to get all developers for dropdowns or selection lists.
        Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync();

        //Task for checking if a developer exists by their ID.
        Task<bool> DeveloperExistsAsync(Guid developerId);

    }
}
