using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly GamingZoneDbContext dbContext;

        public PublisherRepository(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Publisher> GetAllPublishersNoTracking()
        {
            IQueryable<Publisher> publishers = dbContext
                                              .Publishers
                                              .AsNoTracking();

            return publishers;
        }

        public IQueryable<Game> GetAllGamesByPublisherNoTracking(Guid publisherId)
        {
            IQueryable<Game> gamesByPublisher = dbContext
                                               .Games
                                               .Include(g => g.Developer)
                                               .Include(g => g.Publisher)
                                               .Where(g => g.PublisherId == publisherId)
                                               .AsNoTracking();

            return gamesByPublisher;
        }

        public Task<bool> CheckIfPublisherExistsAsync(Guid publisherId)
        {
            return dbContext.Publishers
                            .AnyAsync(p => p.Id == publisherId);
        }
    }
}
