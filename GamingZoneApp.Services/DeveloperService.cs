using GamingZoneApp.Data;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;


namespace GamingZoneApp.Services.Core
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        //Task for viewing all developers with their info.
        public async Task<IEnumerable<AllDevelopersViewModel>> GetAllDevelopersWithInfoAsync()
        {            

            //Projecting the developers to the DeveloperAllDto, ordering by name and the count of games developed using DeveloperRepository method.
            IEnumerable<DeveloperAllDto> developersDto = await developerRepository
                                                              .GetAllDevelopersNoTracking()
                                                              .Include(d => d.GamesDeveloped)
                                                              .Select(d => new DeveloperAllDto
                                                              {
                                                                  Id = d.Id,
                                                                  Name = d.Name,
                                                                  GamesDeveloped = d.GamesDeveloped.Count,
                                                                  Description = d.Description,
                                                                  ImageUrl = d.ImageUrl ?? null,
                                                              })
                                                              .OrderBy(d => d.Name)
                                                              .ThenByDescending(d => d.GamesDeveloped)
                                                              .ToListAsync();

            // Projecting the DeveloperAllDto to the AllDevelopersViewModel.
            IEnumerable<AllDevelopersViewModel> allDevelopersViewModel = developersDto.Select(d => new AllDevelopersViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                GamesDeveloped = d.GamesDeveloped,
                ImageUrl = d.ImageUrl
            });

            return allDevelopersViewModel;
        }

        //Task for viewing all games by a specific developer.
        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesByDeveloperIdAsync(Guid developerId)
        {
            // Projecting the games to the GameAllDto, ordering by title using DeveloperRepository method.
            IEnumerable<GameAllDto> gamesByDev = await developerRepository
                                                      .GetAllGamesByDeveloperNoTracking(developerId)
                                                      .Select(g => new GameAllDto
                                                      {
                                                          Id = g.Id,
                                                          Title = g.Title,
                                                          ImageUrl = g.ImageUrl ?? null,
                                                          Genre = g.Genre.ToString(),
                                                          Developer = g.Developer.Name,
                                                      
                                                      })
                                                      .OrderBy(g => g.Title)
                                                      .ToListAsync();

            IEnumerable<AllGamesViewModel> allGamesByDev = gamesByDev.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl,
                Genre = g.Genre,
                Developer = g.Developer
            });

            return allGamesByDev;
        }

        //Task for viewing all developers for dropdowns in forms.
        public async Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync()
        {
            // Projecting the developers to the AddGameDeveloperViewModel using DeveloperRepository, ordering by name.
            return await developerRepository
                        .GetAllDevelopersNoTracking()
                        .Select(d => new AddGameDeveloperViewModel
                        {
                            Id = d.Id,
                            Name = d.Name
                        })
                        .OrderBy(d => d.Name)
                        .ToListAsync();
        }

        //Task for checking if a developer exists by their ID using DeveloperRepository task.
        public async Task<bool> DeveloperExistsAsync(Guid developerId)
        {
            return await developerRepository.CheckIfDeveloperExistsAsync(developerId);
        }

    }
}
