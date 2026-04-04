using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

using MockQueryable;
using Moq;  
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class DeveloperServiceTests
    {
        private Mock<IDeveloperRepository> developerRepositoryMock;
        private IDeveloperService developerService;

        [SetUp]
        public void SetUp()
        {
            developerRepositoryMock = new Mock<IDeveloperRepository>();
            developerService = new DeveloperService(developerRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllDevelopersWithInfoAsync_WithDevelopers_ReturnsMappedAndOrdered()
        {
            // Arrange
            List<Developer> developers = new List<Developer>
            {
                new Developer
                {
                    Id = Guid.NewGuid(),
                    Name = "TestDeveloperII",
                    Description = "A test studio",
                    ImageUrl = "https://example.com/dev2.png",
                    GamesDeveloped = new List<Game> { new Game(), new Game() }
                },
                new Developer
                {
                    Id = Guid.NewGuid(),
                    Name = "TestDeveloperI",
                    Description = "Another studio",
                    ImageUrl = "https://example.com/dev1.png",
                    GamesDeveloped = new List<Game> { new Game() }
                }
            };

            developerRepositoryMock.Setup(dr => dr.GetAllDevelopersNoTracking())
                                   .Returns(developers.AsQueryable().BuildMock());

            // Act
            List<AllDevelopersViewModel> result = (await developerService.GetAllDevelopersWithInfoAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("TestDeveloperI"));
            Assert.That(result[1].Name, Is.EqualTo("TestDeveloperII"));
            Assert.That(result[1].GamesDeveloped, Is.EqualTo(2));
            Assert.That(result[1].ImageUrl, Is.EqualTo("https://example.com/dev2.png"));
        }

        [Test]
        public async Task GetAllDevelopersWithInfoAsync_NoDevelopers_ReturnsEmpty()
        {
            // Arrange
            developerRepositoryMock.Setup(dr => dr.GetAllDevelopersNoTracking())
                                   .Returns(new List<Developer>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllDevelopersViewModel> result = await developerService.GetAllDevelopersWithInfoAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllDevelopersWithInfoAsync_NullImageUrl_ReturnsNullImageUrl()
        {
            // Arrange
            List<Developer> developers = new List<Developer>
            {
                new Developer
                {
                    Id = Guid.NewGuid(),
                    Name = "TestDeveloper",
                    Description = "Desc",
                    ImageUrl = null,
                    GamesDeveloped = new List<Game>()
                }
            };

            developerRepositoryMock.Setup(dr => dr.GetAllDevelopersNoTracking())
                                   .Returns(developers.AsQueryable().BuildMock());

            // Act
            List<AllDevelopersViewModel> result = (await developerService.GetAllDevelopersWithInfoAsync()).ToList();

            // Assert
            Assert.That(result.First().ImageUrl, Is.Null);
        }

        [Test]
        public async Task GetAllGamesByDeveloperIdAsync_WithGames_ReturnsMappedAndOrdered()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = new Developer { Id = developerId, Name = "TestDev" };

            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "TestGameII",
                    Genre = Genre.Adventure,
                    ImageUrl = "https://example.com/game2.png",
                    Developer = developer
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "TestGameI",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/game1.png",
                    Developer = developer
                }
            };

            developerRepositoryMock.Setup(dr => dr.GetAllGamesByDeveloperNoTracking(developerId))
                                   .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await developerService.GetAllGamesByDeveloperIdAsync(developerId)).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("TestGameI"));
            Assert.That(result[1].Title, Is.EqualTo("TestGameII"));
            Assert.That(result[0].Genre, Is.EqualTo("ActionRPG"));
            Assert.That(result[0].Developer, Is.EqualTo("TestDev"));
        }

        [Test]
        public async Task GetAllGamesByDeveloperIdAsync_NoGames_ReturnsEmpty()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.GetAllGamesByDeveloperNoTracking(developerId))
                                   .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await developerService.GetAllGamesByDeveloperIdAsync(developerId);

            // Assert
            Assert.That(result, Is.Empty);
        }


        [Test]
        public async Task GetAllDevelopersAsync_WithDevelopers_ReturnsMappedAndOrdered()
        {
            // Arrange
            List<Developer> developers = new List<Developer>
            {
                new Developer { Id = Guid.NewGuid(), Name = "TestDeveloperII", Description = "Desc" },
                new Developer { Id = Guid.NewGuid(), Name = "TestDeveloperI", Description = "Desc" }
            };

            developerRepositoryMock.Setup(dr => dr.GetAllDevelopersNoTracking())
                                   .Returns(developers.AsQueryable().BuildMock());

            // Act
            List<AddGameDeveloperViewModel> result = (await developerService.GetAllDevelopersAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("TestDeveloperI"));
            Assert.That(result[1].Name, Is.EqualTo("TestDeveloperII"));
        }

        [Test]
        public async Task GetAllDevelopersAsync_NoDevelopers_ReturnsEmpty()
        {
            // Arrange
            developerRepositoryMock.Setup(dr => dr.GetAllDevelopersNoTracking())
                                   .Returns(new List<Developer>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AddGameDeveloperViewModel> result = await developerService.GetAllDevelopersAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }


        [Test]
        public async Task DeveloperExistsAsync_DeveloperExists_ReturnsTrue()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.CheckIfDeveloperExistsAsync(developerId))
                                   .ReturnsAsync(true);

            // Act
            bool result = await developerService.DeveloperExistsAsync(developerId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeveloperExistsAsync_DeveloperDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.CheckIfDeveloperExistsAsync(developerId))
                                   .ReturnsAsync(false);

            // Act
            bool result = await developerService.DeveloperExistsAsync(developerId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
