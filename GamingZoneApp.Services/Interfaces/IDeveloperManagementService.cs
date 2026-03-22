using GamingZoneApp.ViewModels.Developer;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IDeveloperManagementService
    {
        public Task<bool> AddDeveloperAsync(DeveloperInputModel inputModel);
    }
}
