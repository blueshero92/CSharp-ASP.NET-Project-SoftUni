using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<AddGamePublisherViewModel>> GetAllPublishersAsync();
        Task<bool> PublisherExistsAsync(Guid publisherId);
    }
}
