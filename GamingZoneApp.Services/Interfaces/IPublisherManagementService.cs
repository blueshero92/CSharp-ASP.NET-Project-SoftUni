using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IPublisherManagementService
    {
        public Task<bool> AddPublisherAsync(PublisherInputModel inputModel);

        public Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId);

        public Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel);
        public Task<DeletePublisherDto?> GetPublisherForDeleteAsync(Guid publisherId);

        public Task<bool> HardDeletePublisherAsync(Guid publisherId);
    }
}
