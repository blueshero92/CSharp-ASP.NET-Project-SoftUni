using GamingZoneApp.Data.Models;

namespace GamingZoneApp.Data.Repository.Interfaces
{
    public interface IDeveloperRepository
    {
        IQueryable<Developer> GetAllDevelopersNoTracking();

        IQueryable<Game> GetAllGamesByDeveloperNoTracking(Guid developerId);

        Task<bool> CheckIfDeveloperExistsAsync(Guid developerId);

        Task<Developer?> GetDeveloperByIdAsync(Guid developerId);

        Task CreateDeveloperAsync(Developer developer);

        Task UpdateDeveloperAsync(Developer developer);

        Task DeleteDeveloperAsync(Developer developer);
    }
}
