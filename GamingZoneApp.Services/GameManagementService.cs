using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Game;


namespace GamingZoneApp.Services.Core
{
    public class GameManagementService : IGameManagementService
    {
        private readonly IGameRepository gameRepository;


        public GameManagementService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;

        }


        public async Task<GameInputModel?> GetEditAsync(Guid gameId)
        {
            Game? gameToEdit = await gameRepository.GetGameAsync(gameId);

            if (gameToEdit == null)
            {
                return null;
            }

            GameInputModel gameInputModel = new GameInputModel
            {
                Title = gameToEdit.Title,
                ReleaseDate = gameToEdit.ReleaseDate,
                Genre = gameToEdit.Genre.ToString(),
                Description = gameToEdit.Description,
                ImageUrl = gameToEdit.ImageUrl,
                DeveloperId = gameToEdit.DeveloperId,
                PublisherId = gameToEdit.PublisherId,
                UserId = gameToEdit.UserId
            };

            return gameInputModel;
        }

        public async Task<bool> PostEditAsync(Guid gameId, GameInputModel inputModel)
        {
            Game? gameToEdit = await gameRepository.GetGameAsync(gameId);

            if (gameToEdit == null)
            {
                return false;
            }

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
                gameToEdit.ImageUrl = inputModel.ImageUrl;
                gameToEdit.DeveloperId = inputModel.DeveloperId;
                gameToEdit.PublisherId = inputModel.PublisherId;

                await gameRepository.EditSelectedGameAsync(gameToEdit);
                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }

        public async Task<DeleteGameDto?> GetDeleteAsync(Guid gameId)
        {
            //Get the game to delete from the database using the provided gameId.
            Game? gameToDelete = await gameRepository.GetGameAsync(gameId);

            //If the game is not found, return null to indicate that the game does not exist.
            if (gameToDelete == null)
            {
                return null;
            }

            //If the game is found, create a DeleteGameDto to return the necessary information for deletion.
            DeleteGameDto deleteGameDto = new DeleteGameDto()
            {
                Title = gameToDelete.Title
            };

            return deleteGameDto;
        }

        public async Task<bool> PostDeleteAsync(Guid gameId, DeleteGameDto deleteGameDto)
        {
            //Get the game to delete from the database using the provided gameId.
            Game? gameToDelete = await gameRepository.GetGameAsync(gameId);

            //If the game is not found, return false to indicate that the deletion was unsuccessful.
            if (gameToDelete == null)
            {
                return false;
            }

            //If the game is found, attempt to soft delete it using the repository's SoftDeleteAsync method.
            try
            {
                await gameRepository.SoftDeleteAsync(gameToDelete);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
