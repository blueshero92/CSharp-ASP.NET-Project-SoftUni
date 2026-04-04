using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class GameManagementServiceTests
    {
        private Mock<IGameRepository> gameRepositoryMock;
        private IGameManagementService gameManagementService;

        private static Game CreateTestGame(Guid? id = null)
        {
            return new Game
            {
                Id = id ?? Guid.NewGuid(),
                Title = "TestGame",
                Genre = Genre.Adventure,
                Description = "Test description",
                ReleaseDate = new DateTime(2024, 1, 1),
                ImageUrl = "https://example.com/test.png",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
        }

        [SetUp]
        public void SetUp()
        {
            gameRepositoryMock = new Mock<IGameRepository>();
            gameManagementService = new GameManagementService(gameRepositoryMock.Object);
        }

        [Test]
        public async Task GetEditAsync_GameExists_ReturnsInputModel()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            GameInputModel? result = await gameManagementService.GetEditAsync(gameId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("TestGame"));
            Assert.That(result.Genre, Is.EqualTo("Adventure"));
            Assert.That(result.Description, Is.EqualTo("Test description"));
            Assert.That(result.ImageUrl, Is.EqualTo("https://example.com/test.png"));
            Assert.That(result.DeveloperId, Is.EqualTo(game.DeveloperId));
            Assert.That(result.PublisherId, Is.EqualTo(game.PublisherId));
        }

        [Test]
        public async Task GetEditAsync_GameDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync((Game?)null);

            // Act
            GameInputModel? result = await gameManagementService.GetEditAsync(gameId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task PostEditAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            GameInputModel inputModel = new GameInputModel
            {
                Title = "UpdatedGame",
                ReleaseDate = DateTime.Now,
                Genre = "RPG",
                Description = "Updated description",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId)).ReturnsAsync(game);
            gameRepositoryMock.Setup(gr => gr.EditSelectedGameAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);

            // Act
            bool result = await gameManagementService.PostEditAsync(gameId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            gameRepositoryMock.Verify(gr => gr.EditSelectedGameAsync(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public async Task PostEditAsync_GameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync((Game?)null);

            // Act
            bool result = await gameManagementService.PostEditAsync(gameId, new GameInputModel());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task PostEditAsync_InvalidGenre_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "InvalidGenre",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId)).ReturnsAsync(game);

            // Act
            bool result = await gameManagementService.PostEditAsync(gameId, inputModel);

            // Assert
            Assert.That(result, Is.False);
            gameRepositoryMock.Verify(gr => gr.EditSelectedGameAsync(It.IsAny<Game>()), Times.Never);
        }

        [Test]
        public async Task PostEditAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "Adventure",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId)).ReturnsAsync(game);
            gameRepositoryMock.Setup(gr => gr.EditSelectedGameAsync(It.IsAny<Game>())).ThrowsAsync(new Exception());

            // Act
            bool result = await gameManagementService.PostEditAsync(gameId, inputModel);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetDeleteAsync_GameExists_ReturnsDeleteViewModel()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync(game);

            // Act
            DeleteGameViewModel? result = await gameManagementService.GetDeleteAsync(gameId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("TestGame"));
        }

        [Test]
        public async Task GetDeleteAsync_GameDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync((Game?)null);

            // Act
            DeleteGameViewModel? result = await gameManagementService.GetDeleteAsync(gameId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task PostDeleteAsync_GameExists_ReturnsTrue()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId)).ReturnsAsync(game);
            gameRepositoryMock.Setup(gr => gr.SoftDeleteAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);

            // Act
            bool result = await gameManagementService.PostDeleteAsync(gameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.True);
            gameRepositoryMock.Verify(gr => gr.SoftDeleteAsync(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public async Task PostDeleteAsync_GameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId))
                              .ReturnsAsync((Game?)null);

            // Act
            bool result = await gameManagementService.PostDeleteAsync(gameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task PostDeleteAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();
            Game game = CreateTestGame(gameId);

            gameRepositoryMock.Setup(gr => gr.GetGameAsync(gameId)).ReturnsAsync(game);
            gameRepositoryMock.Setup(gr => gr.SoftDeleteAsync(It.IsAny<Game>())).ThrowsAsync(new Exception());

            // Act
            bool result = await gameManagementService.PostDeleteAsync(gameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.False);
        }
    }
}