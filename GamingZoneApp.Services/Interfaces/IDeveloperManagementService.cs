using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperManagementService
    {
        Task<bool> AddDeveloperAsync(DeveloperInputModel inputModel);

        Task<DeveloperInputModel?> GetDeveloperForEditAsync(Guid developerId);

        Task<bool> EditDeveloperAsync(Guid developerId, DeveloperInputModel inputModel);

        Task<DeleteDeveloperViewModel?> GetDeveloperForDeleteAsync(Guid developerId);

        Task<bool> HardDeleteDeveloperAsync(Guid developerId);
    }
}
