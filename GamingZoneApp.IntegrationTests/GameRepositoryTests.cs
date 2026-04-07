using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GamingZoneApp.IntegrationTests
{
    [TestFixture]
    public class GameRepositoryTests
    {
        private GamingZoneDbContext dbContext;
        private GameRepository gameRepository;

        private static readonly Guid TestUserId = Guid.NewGuid();
        private static readonly Guid TestDeveloperId = Guid.NewGuid();
        private static readonly Guid TestPublisherId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<GamingZoneDbContext> options =
                new DbContextOptionsBuilder<GamingZoneDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

            dbContext = new GamingZoneDbContext(options);
            gameRepository = new GameRepository(dbContext);

            SeedTestData();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private void SeedTestData()
        {
            Developer dev = new Developer
            {
                Id = TestDeveloperId,
                Name = "TestDev",
                Description = "Test developer"
            };

            Publisher pub = new Publisher
            {
                Id = TestPublisherId,
                Name = "TestPub",
                Description = "Test publisher"
            };

            dbContext.Developers.Add(dev);
            dbContext.Publishers.Add(pub);
            dbContext.SaveChanges();
        }

        private Game CreateTestGame(Guid? id = null, string title = "TestGame", bool isDeleted = false)
        {
            return new Game
            {
                Id = id ?? Guid.NewGuid(),
                Title = title,
                Genre = Genre.Adventure,
                Description = "Test description",
                ImageUrl = "https://example.com/test.png",
                ReleaseDate = new DateTime(2023, 1, 1),
                DeveloperId = TestDeveloperId,
                PublisherId = TestPublisherId,
                UserId = TestUserId,
                IsDeleted = isDeleted
            };
        }

        [Test]
        public async Task GetAllGamesNoTrackingAsync_ReturnsNonDeletedGames()
        {
            // Arrange
            Game activeGame = CreateTestGame(title: "ActiveGame");
            Game deletedGame = CreateTestGame(title: "DeletedGame", isDeleted: true);

            dbContext.Games.AddRange(activeGame, deletedGame);

            await dbContext.SaveChangesAsync();

            // Act
            List<Game> result = await gameRepository.GetAllGamesNoTrackingAsync().ToListAsync();

            // Assert
            Assert.That(result.Any(g => g.Title == "ActiveGame"), Is.True);
            Assert.That(result.Any(g => g.Title == "DeletedGame"), Is.False);
        }

        [Test]
        public async Task GetGameByIdNoTracking_GameExists_ReturnsGameWithIncludes()
        {
            // Arrange
            Game game = CreateTestGame();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act
            Game? result = await gameRepository.GetGameByIdNoTracking(game.Id).SingleOrDefaultAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Developer, Is.Not.Null);
            Assert.That(result.Publisher, Is.Not.Null);
        }

        [Test]
        public async Task GetGameByIdNoTracking_GameDoesNotExist_ReturnsNull()
        {
            // Act
            Game? result = await gameRepository.GetGameByIdNoTracking(Guid.NewGuid()).SingleOrDefaultAsync();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddToFavoritesAsync_AddsEntry()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            // Act
            bool result = await gameRepository.AddToFavoritesAsync(gameId, TestUserId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(await dbContext.ApplicationUsersGames.AnyAsync(
                aug => aug.GameId == gameId && aug.UserId == TestUserId), Is.True);
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_EntryExists_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            dbContext.ApplicationUsersGames.Add(new ApplicationUserGame
            {
                GameId = gameId,
                UserId = TestUserId
            });
            await dbContext.SaveChangesAsync();

            // Act
            bool result = await gameRepository.RemoveFromFavoritesAsync(gameId, TestUserId);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(await dbContext.ApplicationUsersGames.AnyAsync(
                aug => aug.GameId == gameId && aug.UserId == TestUserId), Is.False);
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_EntryDoesNotExist_ReturnsFalse()
        {
            // Act
            bool result = await gameRepository.RemoveFromFavoritesAsync(Guid.NewGuid(), TestUserId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetGamesByUserIdNoTracking_ReturnsGamesForUser()
        {
            // Arrange
            Guid otherUserId = Guid.NewGuid();

            Game userGame = CreateTestGame(title: "UserGame");
            Game otherGame = CreateTestGame(title: "OtherGame");

            otherGame.UserId = otherUserId;

            dbContext.Games.AddRange(userGame, otherGame);
            await dbContext.SaveChangesAsync();

            // Act
            List<Game> result = await gameRepository.GetGamesByUserIdNoTracking(TestUserId).ToListAsync();

            // Assert
            Assert.That(result.All(g => g.UserId == TestUserId), Is.True);
            Assert.That(result.Any(g => g.Title == "UserGame"), Is.True);
        }

        [Test]
        public async Task GetFavoriteGamesByUserIdNoTracking_ReturnsFavoritesOnly()
        {
            // Arrange
            Game favGame = CreateTestGame(title: "FavGame");
            Game nonFavGame = CreateTestGame(title: "NonFavGame");

            dbContext.Games.AddRange(favGame, nonFavGame);

            dbContext.ApplicationUsersGames.Add(new ApplicationUserGame
            {
                GameId = favGame.Id,
                UserId = TestUserId
            });
            await dbContext.SaveChangesAsync();

            // Act
            List<Game> result = await gameRepository.GetFavoriteGamesByUserIdNoTracking(TestUserId).ToListAsync();

            // Assert
            Assert.That(result.Any(g => g.Title == "FavGame"), Is.True);
            Assert.That(result.Any(g => g.Title == "NonFavGame"), Is.False);
        }

        [Test]
        public async Task CreateGameAsync_AddsGameToDatabase()
        {
            // Arrange
            Game game = CreateTestGame(title: "NewGame");

            // Act
            await gameRepository.CreateGameAsync(game);

            // Assert
            Assert.That(await dbContext.Games.AnyAsync(g => g.Title == "NewGame"), Is.True);
        }

        [Test]
        public async Task GetGameAsync_GameExists_ReturnsWithIncludes()
        {
            // Arrange
            Game game = CreateTestGame();

            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            // Act
            Game? result = await gameRepository.GetGameAsync(game.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Developer, Is.Not.Null);
            Assert.That(result.Publisher, Is.Not.Null);
        }

        [Test]
        public async Task GetGameAsync_GameDoesNotExist_ReturnsNull()
        {
            // Act
            Game? result = await gameRepository.GetGameAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditSelectedGameAsync_UpdatesGame()
        {
            // Arrange
            Game game = CreateTestGame(title: "OriginalTitle");

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            dbContext.Entry(game).State = EntityState.Detached;

            game.Title = "UpdatedTitle";

            // Act
            await gameRepository.EditSelectedGameAsync(game);

            // Assert
            Game? updated = await dbContext.Games.FindAsync(game.Id);
            Assert.That(updated!.Title, Is.EqualTo("UpdatedTitle"));
        }

        [Test]
        public async Task SoftDeleteAsync_SetsIsDeletedToTrue()
        {
            // Arrange
            Game game = CreateTestGame();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act
            await gameRepository.SoftDeleteAsync(game);

            // Assert — use IgnoreQueryFilters to find the soft-deleted game
            Game? deleted = await dbContext.Games.IgnoreQueryFilters()
                                          .FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.That(deleted!.IsDeleted, Is.True);
        }

        [Test]
        public async Task HardDeleteAsync_RemovesGameFromDatabase()
        {
            // Arrange
            Game game = CreateTestGame();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act
            await gameRepository.HardDeleteAsync(game);

            // Assert
            Game? result = await dbContext.Games.IgnoreQueryFilters()
                                          .FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CheckIfGameExistsAsync_GameExists_ReturnsTrue()
        {
            // Arrange
            Game game = CreateTestGame();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.That(await gameRepository.CheckIfGameExistsAsync(game.Id), Is.True);
        }

        [Test]
        public async Task CheckIfGameExistsAsync_GameDoesNotExist_ReturnsFalse()
        {
            // Act & Assert
            Assert.That(await gameRepository.CheckIfGameExistsAsync(Guid.NewGuid()), Is.False);
        }

        [Test]
        public async Task CheckIfUserIsCreatorAsync_IsCreator_ReturnsTrue()
        {
            // Arrange
            Game game = CreateTestGame();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.That(await gameRepository.CheckIfUserIsCreatorAsync(game.Id, TestUserId), Is.True);
        }

        [Test]
        public async Task CheckIfUserIsCreatorAsync_IsNotCreator_ReturnsFalse()
        {
            // Arrange
            Game game = CreateTestGame();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.That(await gameRepository.CheckIfUserIsCreatorAsync(game.Id, Guid.NewGuid()), Is.False);
        }


        [Test]
        public async Task CheckIfGameIsInFavoritesAsync_InFavorites_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            dbContext.ApplicationUsersGames.Add(new ApplicationUserGame
            {
                GameId = gameId,
                UserId = TestUserId
            });

            await dbContext.SaveChangesAsync();

            // Act & Assert
            Assert.That(await gameRepository.CheckIfGameIsInFavoritesAsync(gameId, TestUserId), Is.True);
        }

        [Test]
        public async Task CheckIfGameIsInFavoritesAsync_NotInFavorites_ReturnsFalse()
        {
            // Act & Assert
            Assert.That(await gameRepository.CheckIfGameIsInFavoritesAsync(Guid.NewGuid(), TestUserId), Is.False);
        }
    }
}