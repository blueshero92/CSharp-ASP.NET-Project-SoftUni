using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IPublisherManagementService
    {
        Task<bool> AddPublisherAsync();

        Task<bool> AddPublisherAsync(PublisherInputModel inputModel);

        Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId);

        Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel);

        Task<DeletePublisherDto?> GetPublisherForDeleteAsync(Guid publisherId);

        Task<bool> HardDeletePublisherAsync(Guid publisherId);
    }
}
