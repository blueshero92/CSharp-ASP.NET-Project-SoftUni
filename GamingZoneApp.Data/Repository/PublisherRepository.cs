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

        public async Task<Publisher?> GetPublisherByIdAsync(Guid publisherId)
        {
            return await dbContext.Publishers
                                  .FirstOrDefaultAsync(p => p.Id == publisherId);
        }

        public async Task CreatePublisherAsync(Publisher publisher)
        {
            dbContext.Publishers.Add(publisher);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            dbContext.Publishers.Update(publisher);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(Publisher publisher)
        {
            dbContext.Publishers.Remove(publisher);
            await dbContext.SaveChangesAsync();

        }
    }
}
