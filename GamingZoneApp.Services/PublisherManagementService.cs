using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core
{
    public class PublisherManagementService : IPublisherManagementService
    {
        public Task<bool> AddPublisherAsync(PublisherInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<DeletePublisherDto?> GetPublisherForDeleteAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }

        public Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardDeletePublisherAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }
    }
}
