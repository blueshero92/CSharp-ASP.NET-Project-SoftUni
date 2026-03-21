using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
