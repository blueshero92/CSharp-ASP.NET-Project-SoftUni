using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        Task<GameViewModel?> GetGameDetailsByIdAsync(Guid id);

        Task<bool> AddGameAsync(GameInputModel model);

        Task<GameInputModel?> GetGameForEditAsync(Guid gameId);

        Task<bool> EditGameAsync(Guid gameId, GameInputModel inputModel);

        Task<DeleteGameViewModel?> GetGameForDeleteAsync(Guid gameId);

        Task<bool> DeleteGameAsync(Guid gameId);

        Task<bool> GameExistsAsync(Guid gameId);
    }
}
