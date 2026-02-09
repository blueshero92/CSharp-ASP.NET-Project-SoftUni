using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Globalization;
using static GamingZoneApp.GCommon.Constants.AppConstants;

namespace GamingZoneApp.Services.Core
{
    public class GameService : IGameService
    {
        private readonly GamingZoneDbContext dbContext;
        public GameService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync()
        {
            //Retrieve all games from the database, including their developers.
            IQueryable<Game> getAllGames = dbContext
                                          .Games
                                          .Include(g => g.Developer)
                                          .AsNoTracking();

            //Project the retrieved games into a collection of AllGamesViewModel, ordered by title.
            IEnumerable<AllGamesViewModel> getAllGamesViewModel = await getAllGames
                                                                       .OrderBy(g => g.Title)
                                                                       .Select(g => new AllGamesViewModel
                                                                       {
                                                                           Id = g.Id,
                                                                           Title = g.Title,
                                                                           ImageUrl = g.ImageUrl,
                                                                           Genre = g.Genre.ToString(),
                                                                           Developer = g.Developer.Name,
                                                                       
                                                                       })
                                                                       .AsNoTracking()
                                                                       .ToListAsync();
            //Return the collection of AllGamesViewModel.
            return getAllGamesViewModel;
        }

        public async Task<GameViewModel> GetGameDetailsByIdAsync(Guid id)
        {
            //Retrieve a game from the database by it's Id, including its developer and publisher.
            GameViewModel? selectedGameViewModel = await dbContext
                                                        .Games
                                                        .Include(g => g.Developer)
                                                        .Include(g => g.Publisher)
                                                        .Where(g => g.Id == id)
                                                        .Select(g => new GameViewModel
                                                        {
                                                            Id = g.Id,
                                                            Title = g.Title,
                                                            ReleaseDate = g.ReleaseDate.ToString(AppDateFormat, CultureInfo.InvariantCulture),
                                                            Genre = g.Genre.ToString(),
                                                            Description = g.Description,
                                                            Rating = g.Rating,
                                                            ImageUrl = g.ImageUrl,
                                                            Developer = g.Developer.Name,
                                                            Publisher = g.Publisher.Name,
                                                            DeveloperLogoUrl = g.Developer.ImageUrl,
                                                            PublisherLogoUrl = g.Publisher.ImageUrl

                                                        })
                                                        .AsNoTracking()
                                                        .SingleOrDefaultAsync();

            //If no game is found with the provided Id, return null.
            //This check is necessary to prevent potential null reference exceptions.
            if (selectedGameViewModel == null)
            {
                return null!;
            }

            //Return the retrieved game projected as a GameViewModel.
            return selectedGameViewModel;
        }
    }
}
