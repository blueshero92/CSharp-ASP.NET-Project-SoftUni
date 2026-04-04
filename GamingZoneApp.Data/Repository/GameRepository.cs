using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GamingZoneDbContext dbContext;

        public GameRepository(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IQueryable<Game> GetAllGamesNoTrackingAsync()
        {
            return dbContext
                  .Games
                  .AsNoTracking();
        }

        public IQueryable<Game> GetGameByIdNoTracking(Guid id)
        {
            IQueryable<Game> game = dbContext
                                   .Games
                                   .AsNoTracking()
                                   .Include(g => g.Developer)
                                   .Include(g => g.Publisher)
                                   .Where(g => g.Id == id);

            return game;
        }

        public async Task<bool> AddToFavoritesAsync(Guid gameId, Guid userId)
        {
            ApplicationUserGame gameToAdd = new ApplicationUserGame
            {
                GameId = gameId,
                UserId = userId
            };

            await dbContext.ApplicationUsersGames.AddAsync(gameToAdd);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromFavoritesAsync(Guid gameId, Guid userId)
        {
            ApplicationUserGame? gameToRemove = dbContext
                                                .ApplicationUsersGames
                                                .SingleOrDefault(au => au.GameId == gameId && au.UserId == userId);

            //If the game is not found in the user's favorites, return false to indicate that there is no game to remove.
            if (gameToRemove == null)
            {
                return false;
            }

            //Remove the game from the user's favorites by deleting the corresponding ApplicationUserGame entity from the database.

            dbContext.ApplicationUsersGames.Remove(gameToRemove);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public IQueryable<Game> GetGamesByUserIdNoTracking(Guid userId)
        {
            IQueryable<Game> games = dbContext
                                    .Games
                                    .Where(g => g.UserId == userId)
                                    .AsNoTracking();

            return games;
        }

        public IQueryable<Game> GetFavoriteGamesByUserIdNoTracking(Guid userId)
        {
            IQueryable<Game> favoriteGames = dbContext
                                            .Games
                                            .Include(g => g.GamesUsers)
                                            .AsNoTracking()
                                            .Where(g => g.GamesUsers
                                                         .Any(gu => gu.UserId == userId));

            return favoriteGames;
        }

        public async Task CreateGameAsync(Game game)
        {
            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Game?> GetGameAsync(Guid gameId)
        {
            Game? gameToEdit = await dbContext
                                        .Games
                                        .Include(g => g.Developer)
                                        .Include(g => g.Publisher)
                                        .SingleOrDefaultAsync(g => g.Id == gameId);

            return gameToEdit;
        }

        public async Task EditSelectedGameAsync(Game game)
        {
            dbContext.Games.Update(game);
            await dbContext.SaveChangesAsync();           
        }

        public async Task SoftDeleteAsync(Game gameToDelete)
        {
            gameToDelete.IsDeleted = true;

            dbContext.Games.Update(gameToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task HardDeleteAsync(Game gameToDelete)
        {
            dbContext.Games.Remove(gameToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfGameExistsAsync(Guid gameId)
        {
            return await dbContext
                        .Games
                        .AnyAsync(g => g.Id == gameId);
        }

        public async Task<bool> CheckIfUserIsCreatorAsync(Guid gameId, Guid userId)
        {
            return await dbContext
                        .Games
                        .AnyAsync(g => g.Id == gameId && g.UserId == userId);
        }

        public async Task<bool> CheckIfGameIsInFavoritesAsync(Guid gameId, Guid userId)
        {
            return await dbContext
                        .ApplicationUsersGames
                        .AnyAsync(au => au.GameId == gameId && au.UserId == userId);
        }
    }
}

