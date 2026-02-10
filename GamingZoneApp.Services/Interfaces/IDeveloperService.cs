using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperService
    {
        //Task for viewing all developers with their info.
        Task<IEnumerable<AllDevelopersViewModel>> GetAllDevelopersWithInfoAsync();

        //Task for viewing all games by a specific developer.
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesByDeveloperIdAsync(Guid developerId);

        //Helper method to get all developers for dropdowns or selection lists.
        Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync();

        //Task for checking if a developer exists by their ID.
        Task<bool> DeveloperExistsAsync(Guid developerId);

    }
}
