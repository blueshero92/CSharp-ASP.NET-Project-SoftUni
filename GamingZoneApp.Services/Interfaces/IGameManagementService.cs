using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Game;


namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameManagementService
    {
        Task<GameInputModel?> GetEditAsync(Guid gameId);

        Task<bool> PostEditAsync(Guid gameId, GameInputModel inputModel);

        Task<DeleteGameDto?> GetDeleteAsync(Guid gameId);

        Task<bool> PostDeleteAsync(Guid gameId, DeleteGameDto deleteGameDto);
    }
}
