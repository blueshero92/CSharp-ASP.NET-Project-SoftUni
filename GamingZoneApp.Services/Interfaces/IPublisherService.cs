using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IPublisherService
    {
        // Task to view all publishers with their info, including the count of games published.
        Task<IEnumerable<AllPublishersViewModel>> GetAllPublishersWithInfoAsync();

        // Task to view all games published by a specific publisher by publisher ID.
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesByPublisherIdAsync(Guid publisherId);

        //Helper method to get all publishers for dropdowns or selection lists.
        Task<IEnumerable<AddGamePublisherViewModel>> GetAllPublishersAsync();

        // Method to check if a publisher exists by its ID, useful for validation before operations.
        Task<bool> PublisherExistsAsync(Guid publisherId);
    }
}
