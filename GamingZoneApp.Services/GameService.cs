using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;

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
                                                                       .ToListAsync();
            //Return the collection of AllGamesViewModel.
            return getAllGamesViewModel;
        }

        public async Task<GameViewModel?> GetGameDetailsByIdAsync(Guid id)
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
                return null;
            }

            //Return the retrieved game projected as a GameViewModel.
            return selectedGameViewModel;
        }

        public async Task<bool> AddGameAsync(GameInputModel inputModel)
        {            
            //Create a new Game entity using the data from the provided GameInputModel.
            try
            {
                if(!Enum.TryParse<Genre>(inputModel.Genre, out Genre genre))
                {
                    return false;
                }

                Game newGame = new Game
                {
                    Title = inputModel.Title,
                    ReleaseDate = inputModel.ReleaseDate,
                    Genre = genre,
                    Description = inputModel.Description,
                    ImageUrl = inputModel.ImageUrl,
                    DeveloperId = inputModel.DeveloperId,
                    PublisherId = inputModel.PublisherId
                };

                await dbContext.Games.AddAsync(newGame);
                await dbContext.SaveChangesAsync();

                return true;
            }
            //If any exception occurs during the process of adding the new game to the database, catch the exception and return false.
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<GameInputModel?> GetGameForEditAsync(Guid gameId)
        {
            //Retrieve the game from the database by its Id.
            Game? gameToEdit = await dbContext
                                        .Games
                                        .Include(g => g.Developer)
                                        .Include(g => g.Publisher)
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(g => g.Id == gameId);
            
            //If no game is found with the provided Id, return null.
            if (gameToEdit == null)
            {
                return null;
            }
           
            //Project the retrieved game into a GameInputModel for editing.
            GameInputModel gameInputModel = new GameInputModel
            {
                Title = gameToEdit.Title,
                ReleaseDate = gameToEdit.ReleaseDate,
                Genre = gameToEdit.Genre.ToString(),
                Description = gameToEdit.Description,
                ImageUrl = gameToEdit.ImageUrl,
                DeveloperId = gameToEdit.DeveloperId,
                PublisherId = gameToEdit.PublisherId
            };
            
            //Return the GameInputModel for editing.
            return gameInputModel;
        }

        public async Task<bool> EditGameAsync(Guid gameId, GameInputModel inputModel)
        {
            //Retrieve the game from the database by its Id.
            Game? gameToEdit = await dbContext
                                    .Games
                                    .Include(g => g.Developer)
                                    .Include(g => g.Publisher)
                                    .SingleOrDefaultAsync(g => g.Id == gameId);

            //If no game is found with the provided Id, return false.
            if (gameToEdit == null)
            {
                return false;
            }

            //Try to update the retrieved game.
            try
            {
                if(!Enum.TryParse<Genre>(inputModel?.Genre, out Genre genre))
                {
                    return false;
                }

                gameToEdit.Title = inputModel.Title;
                gameToEdit.ReleaseDate = inputModel.ReleaseDate;
                gameToEdit.Genre = genre;
                gameToEdit.Description = inputModel.Description;
                gameToEdit.ImageUrl = inputModel.ImageUrl;
                gameToEdit.DeveloperId = inputModel.DeveloperId;
                gameToEdit.PublisherId = inputModel.PublisherId;

                dbContext.Games.Update(gameToEdit);
                await dbContext.SaveChangesAsync();

                return true;
            }
            //If any exception occurs during the process of updating the game in the database, catch the exception and return false.
            catch (Exception)
            {                
                return false;
            }
        }

        public async Task<DeleteGameViewModel?> GetGameForDeleteAsync(Guid gameId)
        {
            //Retrieve the game to be deleted.
            Game? gameToDelete = await dbContext
                                      .Games
                                      .Include(g => g.Developer)
                                      .Include(g => g.Publisher)
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(g => g.Id == gameId);

            if(gameToDelete == null)
            {
                return null;
            }

            DeleteGameViewModel? deleteGameViewModel = new DeleteGameViewModel
            {
                Title = gameToDelete.Title
            };

            return deleteGameViewModel;

        }

        public async Task<bool> DeleteGameAsync(Guid gameId)
        {
            Game? gameToDelete = await dbContext
                                      .Games
                                      .Include(g => g.Developer)
                                      .Include(g => g.Publisher)
                                      .SingleOrDefaultAsync(g => g.Id == gameId);

            if(gameToDelete == null)
            {
                return false;
            }

            try
            {
                dbContext.Games.Remove(gameToDelete);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GameExistsAsync(Guid gameId)
        {
            return await dbContext
                        .Games
                        .AnyAsync(g => g.Id == gameId);
        }

    }
}
