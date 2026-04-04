using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;

using System.Globalization;

using static GamingZoneApp.GCommon.Constants.AppConstants;

namespace GamingZoneApp.Services.Core
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        //Task for viewing all games with their info.
        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync()
        {

            //Project the retrieved games into a collection of AllGamesViewModel, ordered by title using GameRepository.
            IEnumerable<GameAllDto> getAllGamesDto = await gameRepository.GetAllGamesNoTrackingAsync()
                                                                         .OrderBy(g => g.Title)
                                                                         .Select(g => new GameAllDto
                                                                         {
                                                                             Id = g.Id,
                                                                             Title = g.Title,
                                                                             ImageUrl = g.ImageUrl ?? null,
                                                                             Genre = g.Genre.ToString(),
                                                                             Developer = g.Developer.Name,
                                                                             Publisher = g.Publisher.Name

                                                                         })
                                                                         .ToListAsync();

            IEnumerable<AllGamesViewModel> allGamesViewModel = getAllGamesDto.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl ?? null,
                Genre = g.Genre,
                Developer = g.Developer,
                Publisher = g.Publisher
            });

            //Return the collection of AllGamesViewModel.
            return allGamesViewModel;
        }

        //Task for searching games by title, genre, developer, or publisher using a search query string.
        public async Task<IEnumerable<AllGamesViewModel>> SearchGamesAsync(string searchQuery)
        {
            //Try to match the search query against the Genre enum values.
            Genre? matchedGenre = Enum.GetValues<Genre>()
                                      .Cast<Genre>()
                                      .FirstOrDefault(g => g.ToString().ToLower().Contains(searchQuery.ToLower()));


            bool doesGenreMatch = Enum.GetValues<Genre>()
                                      .Cast<Genre>()
                                      .Any(g => g.ToString().ToLower().Contains(searchQuery.ToLower()));

            //Project the retrieved games that match the search query into a collection of GameAllDto, ordered by title using GameRepository.
            IEnumerable <GameAllDto> searchedGameDto = await gameRepository
                                                        .GetAllGamesNoTrackingAsync()
                                                        .Where(g => g.Title.ToLower().Contains(searchQuery.ToLower()) ||
                                                                    (doesGenreMatch && g.Genre == matchedGenre) ||
                                                                    g.Developer.Name.Contains(searchQuery) ||
                                                                    g.Publisher.Name.Contains(searchQuery))
                                                        .OrderBy(g => g.Title)
                                                        .Select(g => new GameAllDto
                                                        {
                                                            Id = g.Id,
                                                            Title = g.Title,
                                                            ImageUrl = g.ImageUrl ?? null,
                                                            Genre = g.Genre.ToString(),
                                                            Developer = g.Developer.Name,
                                                            Publisher = g.Publisher.Name
                                                        })
                                                        .ToListAsync();

            //Project the retrieved GameAllDto into a collection of AllGamesViewModel for presentation in the view.
            IEnumerable<AllGamesViewModel> searchedGamesViewModel = searchedGameDto.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl ?? null,
                Genre = g.Genre,
                Developer = g.Developer,
                Publisher = g.Publisher
            });

            return searchedGamesViewModel;
        }

        //Task for viewing the details of a specific game by its Id.
        public async Task<GameViewModel?> GetGameDetailsByIdAsync(Guid id)
        {
            //Retrieve a game from the database by it's Id, including its developer and publisher using GameRepository.
            GameDetailsDto? selectedGameDto = await gameRepository
                                                        .GetGameByIdNoTracking(id)
                                                        .Select(g => new GameDetailsDto
                                                        {
                                                            Id = g.Id,
                                                            Title = g.Title,
                                                            ReleaseDate = g.ReleaseDate.ToString(AppDateFormat, CultureInfo.InvariantCulture),
                                                            Genre = g.Genre.ToString(),
                                                            Description = g.Description,
                                                            Rating = g.Rating,
                                                            ImageUrl = g.ImageUrl ?? null,
                                                            Developer = g.Developer.Name,
                                                            Publisher = g.Publisher.Name,
                                                            DeveloperLogoUrl = g.Developer.ImageUrl ?? string.Empty,
                                                            PublisherLogoUrl = g.Publisher.ImageUrl ?? string.Empty

                                                        })
                                                        .SingleOrDefaultAsync();

            //If no game is found with the provided Id, return null.
            //This check is necessary to prevent potential null reference exceptions.
            if (selectedGameDto == null)
            {
                return null;
            }

            //Map the retrieved GameDetailsDto to a GameViewModel for presentation in the view.
            GameViewModel gameViewModel = new GameViewModel
            {
                Id = selectedGameDto.Id,
                Title = selectedGameDto.Title,
                ReleaseDate = selectedGameDto.ReleaseDate,
                Genre = selectedGameDto.Genre,
                Description = selectedGameDto.Description,
                Rating = selectedGameDto.Rating,
                ImageUrl = selectedGameDto.ImageUrl ?? null,
                Developer = selectedGameDto.Developer,
                Publisher = selectedGameDto.Publisher,
                DeveloperLogoUrl = selectedGameDto.DeveloperLogoUrl ?? string.Empty,
                PublisherLogoUrl = selectedGameDto.PublisherLogoUrl ?? string.Empty
            };

            //Return the retrieved game projected as a GameViewModel.
            return gameViewModel;
        }

        //Task for adding a game to a user's favorites by the game's Id and the user's Id.
        public async Task<bool> AddGameToFavoritesAsync(Guid gameId, Guid userId)
        {

            //Try to add the game to the user's favorites by creating a new ApplicationUserGame entity and saving it to the database using GameRepository.
            try
            {
                await gameRepository.AddToFavoritesAsync(gameId, userId);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        //Task for removing a game from a user's favorites by the game's Id and the user's Id.
        public async Task<bool> RemoveGameFromFavoritesAsync(Guid gameId, Guid userId)
        {
            try
            {
                //Try to remove the game from the user's favorites by finding the corresponding ApplicationUserGame entity and removing it from the database using GameRepository.
                await gameRepository.RemoveFromFavoritesAsync(gameId, userId);

                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }
        //Task for viewing all games added by a specific user by the user's Id.
        public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesByUserIdAsync(Guid userId)
        {
            //Project the retrieved games added by the specific user into a collection of AllGamesViewModel, ordered by title using GameRepository.
            IEnumerable<GameAllDto> getAllGamesByUserId = await gameRepository
                                                               .GetGamesByUserIdNoTracking(userId)
                                                               .Select(g => new GameAllDto
                                                               {
                                                                   Id = g.Id,
                                                                   Title = g.Title,
                                                                   ImageUrl = g.ImageUrl ?? null,
                                                                   Genre = g.Genre.ToString(),
                                                                   Developer = g.Developer.Name,
                                                                   Publisher = g.Publisher.Name
                                                               })
                                                               .OrderBy(g => g.Title)
                                                               .ToListAsync();

            IEnumerable<AllGamesViewModel> allGamesByUserId = getAllGamesByUserId.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl ?? null,
                Genre = g.Genre,
                Developer = g.Developer,
                Publisher = g.Publisher
            });

            return allGamesByUserId;
        }

        //Task for viewing all games in a user's favorites by the user's Id.
        public async Task<IEnumerable<AllGamesViewModel>> GetFavoriteGamesByUserIdAsync(Guid userId)
        {
            //Project the retrieved favorite games into a collection of GameAllDto, ordered by title using GameRepository.
            IEnumerable<GameAllDto> favoriteGamesDto = await gameRepository
                                                                          .GetFavoriteGamesByUserIdNoTracking(userId)
                                                                          .OrderBy(g => g.Title)
                                                                          .Select(g => new GameAllDto
                                                                          {
                                                                              Id = g.Id,
                                                                              Title = g.Title,
                                                                              ImageUrl = g.ImageUrl ?? null,
                                                                              Genre = g.Genre.ToString(),
                                                                              Developer = g.Developer.Name,
                                                                              Publisher = g.Publisher.Name
                                                                          })
                                                                          .ToListAsync();

            IEnumerable<AllGamesViewModel> favoriteGamesViewModel = favoriteGamesDto.Select(g => new AllGamesViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl ?? null,
                Genre = g.Genre,
                Developer = g.Developer,
                Publisher = g.Publisher
            });

            return favoriteGamesViewModel;

        }

        //Task for adding a new game to the database.
        public async Task<bool> AddGameAsync(GameInputModel inputModel, Guid userId)
        {
            //Create a new Game entity using the data from the provided GameInputModel.
            try
            {
                if (!Enum.TryParse<Genre>(inputModel.Genre, out Genre genre))
                {
                    return false;
                }

                Game newGame = new Game
                {
                    Title = inputModel.Title,
                    ReleaseDate = inputModel.ReleaseDate,
                    Genre = genre,
                    Description = inputModel.Description,
                    ImageUrl = inputModel.ImageUrl ?? null,
                    DeveloperId = inputModel.DeveloperId,
                    PublisherId = inputModel.PublisherId,
                    UserId = userId
                };


                //Try to add the new game to the database using GameRepository.
                await gameRepository.CreateGameAsync(newGame);

                return true;
            }
            //If any exception occurs during the process of adding the new game to the database, catch the exception and return false.
            catch (Exception)
            {
                return false;
            }

        }

        //Task for retrieving a game by its Id and loading it into the form for editing.
        public async Task<GameInputModel?> GetGameForEditAsync(Guid gameId, Guid userId)
        {
            //Retrieve the game from the database by its Id using GameRepository task.
            Game? gameToEdit = await gameRepository.GetGameAsync(gameId);

            //Additional validation to ensure that the user attempting to edit the game is the creator of the game.
            if (gameToEdit?.UserId != userId)
            {
                return null;
            }

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
                ImageUrl = gameToEdit.ImageUrl ?? null,
                DeveloperId = gameToEdit.DeveloperId,
                PublisherId = gameToEdit.PublisherId,
                UserId = gameToEdit.UserId

            };

            //Return the GameInputModel for editing.
            return gameInputModel;
        }

        //Task for updating an existing game in the database using the provided GameInputModel.
        public async Task<bool> EditGameAsync(Guid gameId, GameInputModel inputModel, Guid userId)
        {
            //Retrieve the game from the database by its Id.
            Game? gameToEdit = await gameRepository.GetGameAsync(gameId);

            //Additional validation to ensure that the user attempting to edit the game is the creator of the game.
            if (gameToEdit?.UserId != userId)
            {
                return false;
            }

            //If no game is found with the provided Id, return false.
            if (gameToEdit == null)
            {
                return false;
            }

            //Try to update the retrieved game.
            try
            {
                if (!Enum.TryParse<Genre>(inputModel?.Genre, out Genre genre))
                {
                    return false;
                }

                gameToEdit.Title = inputModel.Title;
                gameToEdit.ReleaseDate = inputModel.ReleaseDate;
                gameToEdit.Genre = genre;
                gameToEdit.Description = inputModel.Description;
                gameToEdit.ImageUrl = inputModel.ImageUrl ?? null;
                gameToEdit.DeveloperId = inputModel.DeveloperId;
                gameToEdit.PublisherId = inputModel.PublisherId;

                //Save the changes to the database using GameRepository task.
                await gameRepository.EditSelectedGameAsync(gameToEdit);

                return true;
            }
            //If any exception occurs during the process of updating the game in the database, catch the exception and return false.
            catch (Exception)
            {
                return false;
            }
        }

        //Task for retrieving a game by its Id and projecting it into a DeleteGameViewModel for deletion confirmation.
        public async Task<DeleteGameViewModel?> GetGameForDeleteAsync(Guid gameId, Guid userId)
        {
            //Retrieve the game to be deleted using GameRepository task.
            Game? gameToDelete = await gameRepository.GetGameAsync(gameId);

            //Additional validation to ensure that the user attempting to delete the game is the creator of the game.
            if (gameToDelete?.UserId != userId)
            {
                return null;
            }

            if (gameToDelete == null)
            {
                return null;
            }


            DeleteGameViewModel? deleteGameViewModel = new DeleteGameViewModel
            {
                Title = gameToDelete.Title
            };

            return deleteGameViewModel;

        }

        //Task for soft deleting a game from the application by setting it's IsDeleted property to true and saving the changes to the database.
        public async Task<bool> SoftDeleteGameAsync(Guid gameId, Guid userId)
        {
            Game? gameToDelete = await gameRepository.GetGameAsync(gameId);

            //Additional validation to ensure that the user attempting to delete the game is the creator of the game.
            if (gameToDelete?.UserId != userId)
            {
                return false;
            }

            if (gameToDelete == null)
            {
                return false;
            }

            try
            {
                //Soft delete the game by setting its IsDeleted property to true and saving the changes to the database using GameRepository task.
                await gameRepository.SoftDeleteAsync(gameToDelete);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Task for deleting a game from the database by its Id.
        public async Task<bool> HardDeleteGameAsync(Guid gameId, Guid userId)
        {
            Game? gameToDelete = await gameRepository.GetGameAsync(gameId);

            //Additional validation to ensure that the user attempting to delete the game is the creator of the game.
            if (gameToDelete?.UserId != userId)
            {
                return false;
            }

            if (gameToDelete == null)
            {
                return false;
            }

            try
            {
                //Hard delete the game from the database using GameRepository task.
                await gameRepository.HardDeleteAsync(gameToDelete);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Task for checking if a game exists in the database by its Id using GameRepository task.
        public async Task<bool> GameExistsAsync(Guid gameId)
        {
            return await gameRepository.CheckIfGameExistsAsync(gameId);
        }

        //Helper task for checking if a user is the creator of a game by comparing the game's UserId with the provided userId using GameRepository task.
        public async Task<bool> IsUserCreatorAsync(Guid gameId, Guid userId)
        {
            return await gameRepository.CheckIfUserIsCreatorAsync(gameId, userId);
        }

        //Helper task for checking if a game is in a user's favorites using GameRepository task.
        public async Task<bool> IsGameInFavoritesAsync(Guid gameId, Guid userId)
        {
            return await gameRepository.CheckIfGameIsInFavoritesAsync(gameId, userId);
        }

    }
}
