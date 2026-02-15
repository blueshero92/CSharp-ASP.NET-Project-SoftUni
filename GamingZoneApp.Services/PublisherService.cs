using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Services.Core
{
    public class PublisherService : IPublisherService
    {
        private readonly GamingZoneDbContext dbContext;

        public PublisherService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Task for viewing all publishers with their info.
        public async Task<IEnumerable<AllPublishersViewModel>> GetAllPublishersWithInfoAsync()
        {
            // Retrieve all publishers from the database, including their published games, and project them into a list of AllPublishersViewModel.
            IEnumerable<AllPublishersViewModel> publishers = await dbContext
                                                                  .Publishers
                                                                  .Include(p => p.GamesPublished)
                                                                  .Select(p => new AllPublishersViewModel
                                                                  {
                                                                      Id = p.Id,
                                                                      Name = p.Name,
                                                                      Description = p.Description,
                                                                      GamesPublished = p.GamesPublished.Count,
                                                                      ImageUrl = p.ImageUrl,
                                                                  })
                                                                  .OrderBy(p => p.Name)
                                                                  .ThenByDescending(p => p.GamesPublished)
                                                                  .AsNoTracking()
                                                                  .ToListAsync();

            return publishers;
        }

        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesByPublisherIdAsync(Guid publisherId)
        {
            // Retrieve all games from the database that are published by the specified publisher, including their developer and publisher information.
            IEnumerable<AllGamesViewModel> gamesByPublisher = await dbContext
                                                                   .Games
                                                                   .Include(g => g.Developer)
                                                                   .Include(g => g.Publisher)
                                                                   .Where(g => g.PublisherId == publisherId)
                                                                   .Select(g => new AllGamesViewModel
                                                                   {
                                                                       Id = g.Id,
                                                                       Title = g.Title,
                                                                       ImageUrl = g.ImageUrl,
                                                                       Genre = g.Genre.ToString(),
                                                                       Developer = g.Developer.Name
                                                                   })
                                                                   .OrderBy(g => g.Title)
                                                                   .AsNoTracking()
                                                                   .ToListAsync();

            return gamesByPublisher;
        }

        //Task for viewing all publishers in the dropdowns for forms.
        public async Task<IEnumerable<AddGamePublisherViewModel>> GetAllPublishersAsync()
        {
            return await dbContext
                        .Publishers
                        .AsNoTracking()
                        .Select(p => new AddGamePublisherViewModel
                        {
                            Id = p.Id,
                            Name = p.Name
                        })
                        .OrderBy (p => p.Name)
                        .ToListAsync();
        }

        //Task for checking if a publisher exists by its Id.
        public async Task<bool> PublisherExistsAsync(Guid publisherId)
        {
            return await dbContext
                        .Publishers.AnyAsync(p => p.Id == publisherId);
        }
    }
}
