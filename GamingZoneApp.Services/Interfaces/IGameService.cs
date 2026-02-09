using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        Task<GameViewModel> GetGameDetailsByIdAsync(Guid id);
    }
}
