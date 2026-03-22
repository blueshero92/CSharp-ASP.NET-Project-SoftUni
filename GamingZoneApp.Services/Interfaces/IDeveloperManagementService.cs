using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.ViewModels.Developer;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperManagementService
    {
        public Task<bool> AddDeveloperAsync(DeveloperInputModel inputModel);

        public Task<DeveloperInputModel?> GetDeveloperForEditAsync(Guid developerId);

        public Task<bool> EditDeveloperAsync(Guid developerId, DeveloperInputModel inputModel);

        public Task<DeleteDeveloperDto?> GetDeveloperForDeleteAsync(Guid developerId);

        public Task<bool> HardDeleteDeveloperAsync(Guid developerId);
    }
}
