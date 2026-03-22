using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.ViewModels.Developer;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperManagementService
    {
        Task<bool> AddDeveloperAsync();

        Task<bool> AddDeveloperAsync(DeveloperInputModel inputModel);

        Task<DeveloperInputModel?> GetDeveloperForEditAsync(Guid developerId);

        Task<bool> EditDeveloperAsync(Guid developerId, DeveloperInputModel inputModel);

        Task<DeleteDeveloperDto?> GetDeveloperForDeleteAsync(Guid developerId);

        Task<bool> HardDeleteDeveloperAsync(Guid developerId);
    }
}
