using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        //Task for viewing all games with their info.
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        //Task for viewing details of a specific game by it's Id.
        Task<GameViewModel?> GetGameDetailsByIdAsync(Guid id);

        //Task for adding a game to the favorites of a user by the game's Id and the user's Id.
        Task<bool> AddGameToFavoritesAsync(Guid gameId, Guid userId);

        //Task for removing a game from the favorites of a user by the game's Id and the user's Id.
        Task<bool> RemoveGameFromFavoritesAsync(Guid gameId, Guid userId);

        //Task for viewing all games added by a specific user by the user's Id.
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesByUserIdAsync(Guid userId);

        //Task for viewing all favorite games of a specific user by the user's Id.
        Task<IEnumerable<AllGamesViewModel>> GetFavoriteGamesByUserIdAsync(Guid userId);

        //Task for adding a new game to the database.
        Task<bool> AddGameAsync(GameInputModel model, Guid userId);

        //Task for getting a game by it's Id and loading the EditGame form.
        Task<GameInputModel?> GetGameForEditAsync(Guid gameId, Guid userId);

        //Task for executing the editing of a game in the database.
        Task<bool> EditGameAsync(Guid gameId, GameInputModel inputModel, Guid userId);

        //Task for getting a game by it's Id and loading the DeleteGame form.
        Task<DeleteGameViewModel?> GetGameForDeleteAsync(Guid gameId, Guid userId);

        //Task for executing the deletion of a game from the database.
        Task<bool> DeleteGameAsync(Guid gameId, Guid userId);

        //Task for checking if a game exists in the database by its Id.
        Task<bool> GameExistsAsync(Guid gameId);

        //Task for checking if a user is the creator of a game by the game's Id and the user's Id.
        Task<bool> IsUserCreatorAsync(Guid gameId, Guid userId);

        //Task for checking if a game is in the favorites of a user by the game's Id and the user's Id.
        Task<bool> IsGameInFavoritesAsync(Guid gameId, Guid userId);

    }
}
