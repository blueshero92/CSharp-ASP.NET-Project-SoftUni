using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;


namespace GamingZoneApp.Services.Core
{
    public class DeveloperService : IDeveloperService
    {
        private readonly GamingZoneDbContext dbContext;

        public DeveloperService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Task for viewing all developers with their info.
        public async Task<IEnumerable<AllDevelopersViewModel>> GetAllDevelopersWithInfoAsync()
        {
            //Building the query to get all developers, including their related GamesDeveloped.
            IQueryable<Developer> allDevelopers = dbContext
                                             .Developers
                                             .Include(d => d.GamesDeveloped);

            //Projecting the developers to the AllDevelopersViewModel, ordering by name and the count of games developed, and materializing the query.
            IEnumerable<AllDevelopersViewModel> developersViewModel = await allDevelopers
                                                                           .Select(d => new AllDevelopersViewModel
                                                                           {
                                                                               Id = d.Id,
                                                                               Name = d.Name,
                                                                               GamesDeveloped = d.GamesDeveloped.Count,
                                                                               Description = d.Description,
                                                                               ImageUrl = d.ImageUrl,
                                                                           })
                                                                           .OrderBy(d => d.Name)
                                                                           .ThenByDescending(d => d.GamesDeveloped)
                                                                           .AsNoTracking()
                                                                           .ToListAsync();

            return developersViewModel;
        }

        //Task for viewing all games by a specific developer.
        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesByDeveloperIdAsync(Guid developerId)
        {
            //Building the query to get games by developer ID, including related Developer and Publisher.
            IQueryable<Game> gamesByDevQuery = dbContext
                                            .Games
                                            .Include(g => g.Developer)
                                            .Include(g => g.Publisher)
                                            .Where(g => g.DeveloperId == developerId);

            // Projecting the games to the AllGamesViewModel, ordering by title, and materializing the query.
            IEnumerable<AllGamesViewModel> gamesByDev = await gamesByDevQuery
                                                             .Select(g => new AllGamesViewModel
                                                             {
                                                                 Id = g.Id,
                                                                 Title = g.Title,
                                                                 ImageUrl = g.ImageUrl,
                                                                 Genre = g.Genre.ToString(),
                                                                 Developer = g.Developer.Name,
                                                             
                                                             })
                                                             .OrderBy(g => g.Title)
                                                             .AsNoTracking()
                                                             .ToListAsync();

            return gamesByDev;
        }

        //Task for viewing all developers for dropdowns in forms.
        public async Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync()
        {           
            return await dbContext
                        .Developers
                        .AsNoTracking()
                        .Select(d => new AddGameDeveloperViewModel
                        {
                            Id = d.Id,
                            Name = d.Name
                        })
                        .OrderBy(d => d.Name)
                        .ToListAsync();
        }

        //Task for checking if a developer exists by their ID.
        public async Task<bool> DeveloperExistsAsync(Guid developerId)
        {
            return await dbContext
                        .Developers.AnyAsync(d => d.Id == developerId);
        }

    }
}
