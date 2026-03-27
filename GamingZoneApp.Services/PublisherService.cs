using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Services.Core
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        //Task for viewing all publishers with their info.
        public async Task<IEnumerable<AllPublishersViewModel>> GetAllPublishersWithInfoAsync()
        {
            // Retrieve all publishers from the database, including their published games, and project them into a list of PublisherAllDto.
            IEnumerable<PublisherAllDto> publishers = await publisherRepository
                                                           .GetAllPublishersNoTracking()
                                                           .Include(p => p.GamesPublished)
                                                           .Select(p => new PublisherAllDto
                                                           {
                                                               Id = p.Id,
                                                               Name = p.Name,
                                                               Description = p.Description,
                                                               GamesPublished = p.GamesPublished.Count,
                                                               ImageUrl = p.ImageUrl ?? null,
                                                           })
                                                           .OrderBy(p => p.Name)
                                                           .ThenByDescending(p => p.GamesPublished)
                                                           .ToListAsync();

            // Map the list of PublisherAllDto to a list of AllPublishersViewModel and return it.
            IEnumerable<AllPublishersViewModel> allPublishersViewModel = publishers.Select(p => new AllPublishersViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                GamesPublished = p.GamesPublished,
                ImageUrl = p.ImageUrl ?? null
            });


            return allPublishersViewModel;
        }

        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesByPublisherIdAsync(Guid publisherId)
        {
            // Retrieve all games from the database that are published by the specified publisher, including their developer and publisher information.
            IEnumerable<GameAllDto> gamesByPublisher = await publisherRepository
                                                            .GetAllGamesByPublisherNoTracking(publisherId)
                                                            .Select(g => new GameAllDto
                                                            {
                                                                Id = g.Id,
                                                                Title = g.Title,
                                                                ImageUrl = g.ImageUrl ?? null,
                                                                Genre = g.Genre.ToString(),
                                                                Developer = g.Developer.Name
                                                            })
                                                            .OrderBy(g => g.Title)
                                                            .ToListAsync();

            // Map the list of GameAllDto to a list of AllGamesViewModel and return it.
            IEnumerable<AllGamesViewModel> allGamesByPublisher = gamesByPublisher.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl ?? null,
                Genre = g.Genre,
                Developer = g.Developer
            });

            return allGamesByPublisher;
        }

        //Task for viewing all publishers in the dropdowns for forms.
        public async Task<IEnumerable<AddGamePublisherViewModel>> GetAllPublishersAsync()
        {
            return await publisherRepository
                        .GetAllPublishersNoTracking()
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
            return await publisherRepository.CheckIfPublisherExistsAsync(publisherId);
        }
    }
}
