using GamingZoneApp.Data.Models;

namespace GamingZoneApp.Data.Repository.Interfaces
{
    public interface IDeveloperRepository
    {
        IQueryable<Developer> GetAllDevelopersNoTracking();

        IQueryable<Game> GetAllGamesByDeveloperNoTracking(Guid developerId);

        Task<bool> CheckIfDeveloperExistsAsync(Guid developerId);
    }
}
