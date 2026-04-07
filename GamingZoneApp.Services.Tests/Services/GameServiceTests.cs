using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using MockQueryable;
using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Services
{
    [TestFixture]
    public class GameServiceTests
    {
        private Mock<IGameRepository> gameRepositoryMock;
        private IGameService gameService;

        [SetUp]
        public void SetUp()
        {
            gameRepositoryMock = new Mock<IGameRepository>();
            gameService = new GameService(gameRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllGamesAsync_WithMultipleGames_ReturnsAllGamesMappedCorrectly()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "TestGameI",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/zelda.png",
                    Developer = new Developer { Name = "TestDeveloper" },
                    Publisher = new Publisher { Name = "TestPublisher" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "TestGameII",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "TestDeveloperII" },
                    Publisher = new Publisher { Name = "TestPublisherII" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await gameService.GetAllGamesAsync();
            List<AllGamesViewModel> resultList = result.ToList();

            // Assert
            Assert.That(resultList, Has.Count.EqualTo(2));

            Assert.That(resultList[0].Title, Is.EqualTo("TestGameI"));
            Assert.That(resultList[1].Title, Is.EqualTo("TestGameII"));

            Assert.That(resultList[1].Genre, Is.EqualTo("ActionRPG"));
            Assert.That(resultList[1].Developer, Is.EqualTo("TestDeveloperII"));
            Assert.That(resultList[1].Publisher, Is.EqualTo("TestPublisherII"));
            Assert.That(resultList[1].ImageUrl, Is.EqualTo("https://example.com/eldenring.png"));
        }

        [Test]
        public async Task GetAllGamesAsync_WithNoGames_ReturnsEmptyCollection()
        {
            // Arrange
            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await gameService.GetAllGamesAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllGamesAsync_WithNullImageUrl_ReturnsNullImageUrl()
        {
            // Arrange
            List<Game> games = new List<Game>()
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = null,
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await gameService.GetAllGamesAsync();
            AllGamesViewModel? game = result.FirstOrDefault();

            // Assert
            Assert.That(game?.ImageUrl, Is.Null);
        }

        [Test]
        public async Task SearchGamesAsync_ByTitle_ReturnsMatchingGame()
        {
            // Arrange
            List<Game> games = new List<Game>()
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeOtherGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDevI" },
                    Publisher = new Publisher { Name = "GamePubI" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.SearchGamesAsync("SomeGame")).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("SomeGame"));
        }

        [Test]
        public async Task SearchGamesAsync_ByGenre_ReturnsMatchingGame()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeOtherGame",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "GameDevI" },
                    Publisher = new Publisher { Name = "GamePubI" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.SearchGamesAsync("Adventure")).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("SomeGame"));
        }

        [Test]
        public async Task SearchGamesAsync_ByDeveloper_ReturnsMatchingGame()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeOtherGame",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "GameDevI" },
                    Publisher = new Publisher { Name = "GamePubI" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.SearchGamesAsync("GameDev")).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("SomeGame"));
        }

        [Test]
        public async Task SearchGamesAsync_ByPublisher_ReturnsMatchingGame()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeOtherGame",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "GameDevI" },
                    Publisher = new Publisher { Name = "GamePubI" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.SearchGamesAsync("GamePub")).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("SomeGame"));
        }

        [Test]
        public async Task SearchGamesAsync_WithNoMatches_ReturnsEmptyCollection()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeGame",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/gtav.png",
                    Developer = new Developer { Name = "GameDev" },
                    Publisher = new Publisher { Name = "GamePub" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "SomeOtherGame",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "GameDevI" },
                    Publisher = new Publisher { Name = "GamePubI" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetAllGamesNoTrackingAsync())
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.SearchGamesAsync("NonExistent")).ToList();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetGameDetailsAsync_GameExists_ReturnsTrue()
        {
            // Arrange
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Title = "TestGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/zelda.png",
                Developer = new Developer { Name = "TestDeveloper" },
                Publisher = new Publisher { Name = "TestPublisher" }
            };

            gameRepositoryMock.Setup(gr => gr.GetGameByIdNoTracking(game.Id))
                              .Returns(new List<Game> { game }.AsQueryable().BuildMock());

            // Act
            GameViewModel? result = await gameService.GetGameDetailsByIdAsync(game.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Title, Is.EqualTo("TestGame"));
            Assert.That(result?.Genre, Is.EqualTo("Adventure"));
            Assert.That(result?.Developer, Is.EqualTo("TestDeveloper"));
            Assert.That(result?.Publisher, Is.EqualTo("TestPublisher"));
        }

        [Test]
        public async Task GetGameDetailsAsync_GameDoesNotFound_ReturnsNull()
        {
            // Arrange
            Guid nonExistentGameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameByIdNoTracking(nonExistentGameId))
                              .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            GameViewModel? result = await gameService.GetGameDetailsByIdAsync(nonExistentGameId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddGameToFavoritesAsync_Success_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.AddToFavoritesAsync(gameId, userId))
                              .ReturnsAsync(true);

            // Act
            bool result = await gameService.AddGameToFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddGameToFavoritesAsync_Failure_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.AddToFavoritesAsync(gameId, userId))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.AddGameToFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveGameFromFavoritesAsync_Success_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.RemoveFromFavoritesAsync(gameId, userId))
                              .ReturnsAsync(true);

            // Act
            bool result = await gameService.RemoveGameFromFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RemoveGameFromFavoritesAsync_Failure_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.RemoveFromFavoritesAsync(gameId, userId))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.RemoveGameFromFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAllGamesByUserIdAsync_WithGames_ReturnsMappedAndOrdered()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Zebra Game",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/zebra.png",
                    Developer = new Developer { Name = "DevA" },
                    Publisher = new Publisher { Name = "PubA" },
                    UserId = userId
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Alpha Game",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/alpha.png",
                    Developer = new Developer { Name = "DevB" },
                    Publisher = new Publisher { Name = "PubB" },
                    UserId = userId
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetGamesByUserIdNoTracking(userId))
                              .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.GetAllGamesByUserIdAsync(userId)).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Alpha Game"));
            Assert.That(result[1].Title, Is.EqualTo("Zebra Game"));
            Assert.That(result[0].Genre, Is.EqualTo("ActionRPG"));
            Assert.That(result[0].Developer, Is.EqualTo("DevB"));
            Assert.That(result[0].Publisher, Is.EqualTo("PubB"));
        }

        [Test]
        public async Task GetAllGamesByUserIdAsync_NoGames_ReturnsEmpty()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGamesByUserIdNoTracking(userId))
                              .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await gameService.GetAllGamesByUserIdAsync(userId);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetFavoriteGamesByUserIdAsync_WithFavorites_ReturnsMappedAndOrdered()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<Game> favoriteGames = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Zelda",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/zelda.png",
                    Developer = new Developer { Name = "Nintendo" },
                    Publisher = new Publisher { Name = "Nintendo" }
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Elden Ring",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = new Developer { Name = "FromSoftware" },
                    Publisher = new Publisher { Name = "Bandai" }
                }
            };

            gameRepositoryMock.Setup(gr => gr.GetFavoriteGamesByUserIdNoTracking(userId))
                              .Returns(favoriteGames.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await gameService.GetFavoriteGamesByUserIdAsync(userId)).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Elden Ring"));
            Assert.That(result[1].Title, Is.EqualTo("Zelda"));
            Assert.That(result[0].Developer, Is.EqualTo("FromSoftware"));
            Assert.That(result[0].Publisher, Is.EqualTo("Bandai"));
        }

        [Test]
        public async Task GetFavoriteGamesByUserIdAsync_NoFavorites_ReturnsEmpty()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetFavoriteGamesByUserIdNoTracking(userId))
                              .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await gameService.GetFavoriteGamesByUserIdAsync(userId);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task AddGameAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "NewGame",
                Genre = "Adventure",
                ImageUrl = "https://example.com/newgame.png",
                Description = "A new exciting adventure game.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.CreateGameAsync(It.IsAny<Game>()))
                              .Returns(Task.CompletedTask);

            // Act
            bool result = await gameService.AddGameAsync(inputModel, Guid.NewGuid());

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddGameAsync_InvalidInput_ReturnsFalse()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "NewGame",
                Genre = "Adventure",
                ImageUrl = "https://example.com/newgame.png",
                Description = "A new exciting adventure game.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.CreateGameAsync(It.IsAny<Game>()))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.AddGameAsync(inputModel, Guid.NewGuid());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AddGameAsync_InvalidGenre_ReturnsFalse()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "NewGame",
                Genre = "NotARealGenre",
                ImageUrl = "https://example.com/newgame.png",
                Description = "A game with an invalid genre.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            // Act
            bool result = await gameService.AddGameAsync(inputModel, Guid.NewGuid());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetGameForEditAsync_ValidCreator_ReturnsInputModel()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "EditableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/editablegame.png",
                Description = "A game that can be edited.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "EditDev" },
                Publisher = new Publisher { Name = "EditPub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            GameInputModel? result = await gameService.GetGameForEditAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Title, Is.EqualTo("EditableGame"));
        }

        [Test]
        public async Task GetGameForEditAsync_InvalidCreator_ReturnsNull()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();
            Guid differentUserId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "EditableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/editablegame.png",
                Description = "A game that can be edited.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "EditDev" },
                Publisher = new Publisher { Name = "EditPub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            GameInputModel? result = await gameService.GetGameForEditAsync(gameId, differentUserId);

            // Assert
            Assert.That(result, Is.Null);
            Assert.That(result?.Title, Is.Not.EqualTo("EditableGame"));
        }

        [Test]
        public async Task EditGameAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            GameInputModel inputModel = new GameInputModel
            {
                Title = "EditedGame",
                Genre = "ActionRPG",
                ImageUrl = "https://example.com/editedgame.png",
                Description = "An edited game.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(new Game { UserId = creatorId });

            gameRepositoryMock.Setup(gr => gr.EditSelectedGameAsync(It.IsAny<Game>()))
                              .Returns(Task.CompletedTask);

            // Act
            bool result = await gameService.EditGameAsync(gameId, inputModel, creatorId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task EditGameAsync_InvalidInput_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            GameInputModel inputModel = new GameInputModel
            {
                Title = "EditedGame",
                Genre = "ActionRPG",
                ImageUrl = "https://example.com/editedgame.png",
                Description = "An edited game.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(new Game { UserId = creatorId });

            gameRepositoryMock.Setup(gr => gr.EditSelectedGameAsync(It.IsAny<Game>()))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.EditGameAsync(gameId, inputModel, creatorId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditGameAsync_UnauthorizedUser_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();
            Guid differentUserId = Guid.NewGuid();

            GameInputModel inputModel = new GameInputModel
            {
                Title = "EditedGame",
                Genre = "ActionRPG",
                ImageUrl = "https://example.com/editedgame.png",
                Description = "An edited game.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(new Game { UserId = creatorId });

            // Act
            bool result = await gameService.EditGameAsync(gameId, inputModel, differentUserId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditGameAsync_InvalidGenre_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            GameInputModel inputModel = new GameInputModel
            {
                Title = "EditedGame",
                Genre = "NotARealGenre",
                ImageUrl = "https://example.com/editedgame.png",
                Description = "An edited game with an invalid genre.",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(new Game { UserId = creatorId });

            // Act
            bool result = await gameService.EditGameAsync(gameId, inputModel, creatorId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetGameForDeleteAsync_ValidCreator_ReturnsDeleteViewModel()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game to be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            DeleteGameViewModel? result = await gameService.GetGameForDeleteAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Title, Is.EqualTo("DeletableGame"));
        }

        [Test]
        public async Task GetGameForDeleteAsync_InvalidCreator_ReturnsNull()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();
            Guid differentUserId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game to be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            DeleteGameViewModel? result = await gameService.GetGameForDeleteAsync(gameId, differentUserId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetGameForDeleteAsync_GameDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync((Game?)null);

            // Act
            DeleteGameViewModel? result = await gameService.GetGameForDeleteAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SoftDeleteGameAsync_ValidCreator_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            gameRepositoryMock.Setup(gr => gr.SoftDeleteAsync(game))
                              .Returns(Task.CompletedTask);

            // Act
            bool result = await gameService.SoftDeleteGameAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task SoftDeleteGameAsync_InvalidCreator_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();
            Guid differentUserId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            bool result = await gameService.SoftDeleteGameAsync(gameId, differentUserId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SoftDeleteGameAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            gameRepositoryMock.Setup(gr => gr.SoftDeleteAsync(game))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.SoftDeleteGameAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task HardDeleteGameAsync_ValidCreator_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            gameRepositoryMock.Setup(gr => gr.HardDeleteAsync(game))
                              .Returns(Task.CompletedTask);

            // Act
            bool result = await gameService.HardDeleteGameAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task HardDeleteGameAsync_InvalidCreator_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();
            Guid differentUserId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            bool result = await gameService.HardDeleteGameAsync(gameId, differentUserId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task HardDeleteGameAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid creatorId = Guid.NewGuid();

            Game game = new Game
            {
                Id = gameId,
                Title = "DeletableGame",
                Genre = Genre.Adventure,
                ImageUrl = "https://example.com/deletablegame.png",
                Description = "A game that can be deleted.",
                ReleaseDate = new DateTime(2020, 1, 1),
                Developer = new Developer { Name = "DeleteDev" },
                Publisher = new Publisher { Name = "DeletePub" },
                UserId = creatorId
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            gameRepositoryMock.Setup(gr => gr.HardDeleteAsync(game))
                              .ThrowsAsync(new Exception());

            // Act
            bool result = await gameService.HardDeleteGameAsync(gameId, creatorId);

            // Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public async Task GameExistsAsync_GameExists_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfGameExistsAsync(gameId))
                              .ReturnsAsync(true);

            // Act
            bool result = await gameService.GameExistsAsync(gameId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GameExistsAsync_GameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfGameExistsAsync(gameId))
                              .ReturnsAsync(false);

            // Act
            bool result = await gameService.GameExistsAsync(gameId);

            // Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public async Task UserIsCreatorAsync_UserIsCreator_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfUserIsCreatorAsync(gameId, userId))
                              .ReturnsAsync(true);

            // Act
            bool result = await gameService.IsUserCreatorAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task UserIsCreatorAsync_UserIsNotCreator_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfUserIsCreatorAsync(gameId, userId))
                              .ReturnsAsync(false);

            // Act
            bool result = await gameService.IsUserCreatorAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GameIsInFavoritesAsync_GameIsInFavorites_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfGameIsInFavoritesAsync(gameId, userId))
                              .ReturnsAsync(true);

            // Act
            bool result = await gameService.IsGameInFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GameIsInFavoritesAsync_GameIsNotInFavorites_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.CheckIfGameIsInFavoritesAsync(gameId, userId))
                              .ReturnsAsync(false);

            // Act
            bool result = await gameService.IsGameInFavoritesAsync(gameId, userId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}