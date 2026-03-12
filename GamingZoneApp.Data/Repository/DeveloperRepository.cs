using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data.Repository
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly GamingZoneDbContext dbContext;

        public DeveloperRepository(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IQueryable<Developer> GetAllDevelopersNoTracking()
        {
            IQueryable<Developer> developers = dbContext
                                              .Developers
                                              .AsNoTracking();
            return developers;
        }

        public IQueryable<Game> GetAllGamesByDeveloperNoTracking(Guid developerId)
        {
            IQueryable<Game> gamesByDeveloper = dbContext
                                               .Games
                                               .Include(g => g.Developer)
                                               .Include(g => g.Publisher)
                                               .Where(g => g.DeveloperId == developerId)
                                               .AsNoTracking();

            return gamesByDeveloper;
        }

        public async Task<bool> CheckIfDeveloperExistsAsync(Guid developerId)
        {
            return await dbContext
                        .Developers.AnyAsync(d => d.Id == developerId);
        }

    }
}
