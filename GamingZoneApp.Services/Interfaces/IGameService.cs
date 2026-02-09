using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        Task<GameViewModel> GetGameDetailsByIdAsync(Guid id);

        Task<bool> AddGameAsync(GameInputModel model);

        Task<GameInputModel> ShowEditGameFormAsync(Guid gameId);

        Task<bool> EditGameAsync(Guid gameId, GameInputModel? inputModel);

        Task<DeleteGameViewModel> ShowDeleteGameFormAsync(Guid gameId);

        Task<bool> DeleteGameAsync(Guid gameId, DeleteGameViewModel? viewModel);

        Task<bool> GameExistsAsync(Guid gameId);
    }
}
