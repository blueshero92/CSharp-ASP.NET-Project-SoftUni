using GamingZoneApp.ViewModels.Game;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameService
    {
        //Task for viewing all games with their info.
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();

        //Task for viewing details of a specific game by it's Id.
        Task<GameViewModel?> GetGameDetailsByIdAsync(Guid id);

        //Task for adding a new game to the database.
        Task<bool> AddGameAsync(GameInputModel model);

        //Task for getting a game by it's Id and loading the EditGame form.
        Task<GameInputModel?> GetGameForEditAsync(Guid gameId);

        //Task for executing the editing of a game in the database.
        Task<bool> EditGameAsync(Guid gameId, GameInputModel inputModel);

        //Task for getting a game by it's Id and loading the DeleteGame form.
        Task<DeleteGameViewModel?> GetGameForDeleteAsync(Guid gameId);

        //Task for executing the deletion of a game from the database.
        Task<bool> DeleteGameAsync(Guid gameId);

        //Task for checking if a game exists in the database by its Id.
        Task<bool> GameExistsAsync(Guid gameId);
    }
}
