using GamingZoneApp.Data.Models;

namespace GamingZoneApp.Data.Repository.Interfaces
{
    public interface IGameRepository
    {
        IQueryable<Game> GetAllGamesNoTrackingAsync();

        IQueryable<Game> GetGameByIdNoTracking(Guid id);

        Task<bool> AddToFavoritesAsync(Guid gameId, Guid userId);

        Task<bool> RemoveFromFavoritesAsync(Guid gameid, Guid userId);

        IQueryable<Game> GetGamesByUserIdNoTracking(Guid userId);

        IQueryable<Game> GetFavoriteGamesByUserIdNoTracking(Guid userId);

        Task CreateGameAsync(Game game);

        Task<Game?> GetGameNoTrackingAsync(Guid gameId);

        Task EditSelectedGameAsync(Game game);

        Task SoftDeleteAsync(Game gameToDelete);

        Task HardDeleteSync (Game gameToDelete);

        Task<bool> CheckIfGameExistsAsync(Guid gameId);

        Task<bool> CheckIfUserIsCreatorAsync(Guid gameId, Guid userId);

        Task<bool> CheckIfGameIsInFavoritesAsync(Guid gameId, Guid userId);
    }
}
