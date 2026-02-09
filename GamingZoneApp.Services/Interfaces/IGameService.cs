using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        Task<GameViewModel> GetGameDetailsByIdAsync(Guid id);

        Task<bool> AddGameAsync(GameInputModel model);

        Task<bool> GameExistsAsync(Guid gameId);
    }
}
