using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Admin.Publisher;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IPublisherManagementService
    {
        Task<bool> AddPublisherAsync(PublisherInputModel inputModel);

        Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId);

        Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel);

        Task<DeletePublisherViewModel?> GetPublisherForDeleteAsync(Guid publisherId);

        Task<bool> HardDeletePublisherAsync(Guid publisherId);
    }
}
