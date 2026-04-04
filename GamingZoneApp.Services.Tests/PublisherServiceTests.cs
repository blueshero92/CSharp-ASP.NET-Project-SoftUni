using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;

using MockQueryable;
using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class PublisherServiceTests
    {
        private Mock<IPublisherRepository> publisherRepositoryMock;
        private IPublisherService publisherService;

        [SetUp]
        public void SetUp()
        {
            publisherRepositoryMock = new Mock<IPublisherRepository>();
            publisherService = new PublisherService(publisherRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllPublishersWithInfoAsync_ShouldDisplayAllPublishersSortedAlphabetically()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher
                {
                    Id = Guid.NewGuid(),
                    Name = "TestPublisherII",
                    Description = "Test publisher II",
                    ImageUrl = "https://example.com/pub2.png",
                    GamesPublished = new List<Game> { new Game(), new Game(), new Game() }
                },
                new Publisher
                {
                    Id = Guid.NewGuid(),
                    Name = "TestPublisherI",
                    Description = "Test publisher I",
                    ImageUrl = "https://example.com/pub1.png",
                    GamesPublished = new List<Game> { new Game() }
                }
            };

            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(publishers.AsQueryable().BuildMock());

            // Act
            List<AllPublishersViewModel> result = (await publisherService.GetAllPublishersWithInfoAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("TestPublisherI"));
            Assert.That(result[1].Name, Is.EqualTo("TestPublisherII"));
        }

        [Test]
        public async Task GetAllPublishersWithInfoAsync_ShouldShowCorrectGameCount()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher
                {
                    Id = Guid.NewGuid(),
                    Name = "TestPublisher",
                    Description = "Test publisher",
                    ImageUrl = "https://example.com/pub.png",
                    GamesPublished = new List<Game> { new Game(), new Game() }
                }
            };

            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(publishers.AsQueryable().BuildMock());

            // Act
            List<AllPublishersViewModel> result = (await publisherService.GetAllPublishersWithInfoAsync()).ToList();

            // Assert
            Assert.That(result.First().GamesPublished, Is.EqualTo(2));
            Assert.That(result.First().Description, Is.EqualTo("Test publisher"));
            Assert.That(result.First().ImageUrl, Is.EqualTo("https://example.com/pub.png"));
        }

        [Test]
        public async Task GetAllPublishersWithInfoAsync_WhenNoPublishersExist_ShouldShowEmptyList()
        {
            // Arrange
            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(new List<Publisher>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllPublishersViewModel> result = await publisherService.GetAllPublishersWithInfoAsync();

            // Assert - a new platform with no publishers should gracefully show nothing
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllPublishersWithInfoAsync_PublisherWithNoLogo_ShouldReturnNullImage()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher
                {
                    Id = Guid.NewGuid(),
                    Name = "Indie Publisher",
                    Description = "Small indie publisher",
                    ImageUrl = null,
                    GamesPublished = new List<Game>()
                }
            };

            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(publishers.AsQueryable().BuildMock());

            // Act
            List<AllPublishersViewModel> result = (await publisherService.GetAllPublishersWithInfoAsync()).ToList();

            // Assert - publisher without a logo should not break the page
            Assert.That(result.First().ImageUrl, Is.Null);
            Assert.That(result.First().GamesPublished, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllGamesByPublisherIdAsync_ShouldReturnAllGamesForPublisherSortedByTitle()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Developer developer = new Developer { Name = "FromSoftware" };

            List<Game> games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Tekken 8",
                    Genre = Genre.Fighting,
                    ImageUrl = "https://example.com/tekken.png",
                    Developer = developer
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Title = "Elden Ring",
                    Genre = Genre.ActionRPG,
                    ImageUrl = "https://example.com/eldenring.png",
                    Developer = developer
                }
            };

            publisherRepositoryMock.Setup(pr => pr.GetAllGamesByPublisherNoTracking(publisherId))
                                   .Returns(games.AsQueryable().BuildMock());

            // Act
            List<AllGamesViewModel> result = (await publisherService.GetAllGamesByPublisherIdAsync(publisherId)).ToList();

            // Assert - games are sorted alphabetically by title
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Elden Ring"));
            Assert.That(result[1].Title, Is.EqualTo("Tekken 8"));
            Assert.That(result[0].Genre, Is.EqualTo("ActionRPG"));
            Assert.That(result[0].Developer, Is.EqualTo("FromSoftware"));
        }

        [Test]
        public async Task GetAllGamesByPublisherIdAsync_PublisherWithNoGames_ShouldReturnEmptyList()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.GetAllGamesByPublisherNoTracking(publisherId))
                                   .Returns(new List<Game>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AllGamesViewModel> result = await publisherService.GetAllGamesByPublisherIdAsync(publisherId);

            // Assert - a publisher who hasn't released any games yet should show an empty catalog
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllPublishersAsync_ShouldReturnDropdownListSortedAlphabetically()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher { Id = Guid.NewGuid(), Name = "Square Enix", Description = "Desc" },
                new Publisher { Id = Guid.NewGuid(), Name = "Activision", Description = "Desc" },
                new Publisher { Id = Guid.NewGuid(), Name = "Electronic Arts", Description = "Desc" }
            };

            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(publishers.AsQueryable().BuildMock());

            // Act
            List<AddGamePublisherViewModel> result = (await publisherService.GetAllPublishersAsync()).ToList();

            // Assert - dropdown should be alphabetical for easy user selection
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[0].Name, Is.EqualTo("Activision"));
            Assert.That(result[1].Name, Is.EqualTo("Electronic Arts"));
            Assert.That(result[2].Name, Is.EqualTo("Square Enix"));
        }

        [Test]
        public async Task GetAllPublishersAsync_NoneExist_ShouldReturnEmptyDropdown()
        {
            // Arrange
            publisherRepositoryMock.Setup(pr => pr.GetAllPublishersNoTracking())
                                   .Returns(new List<Publisher>().AsQueryable().BuildMock());

            // Act
            IEnumerable<AddGamePublisherViewModel> result = await publisherService.GetAllPublishersAsync();

            // Assert - no publishers means empty dropdown, form should still render
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task PublisherExistsAsync_ValidPublisher_ShouldConfirmExistence()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.CheckIfPublisherExistsAsync(publisherId))
                                   .ReturnsAsync(true);

            // Act
            bool result = await publisherService.PublisherExistsAsync(publisherId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task PublisherExistsAsync_NonExistentPublisher_ShouldRejectAssignment()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.CheckIfPublisherExistsAsync(publisherId))
                                   .ReturnsAsync(false);

            // Act
            bool result = await publisherService.PublisherExistsAsync(publisherId);

            // Assert - prevents assigning a deleted or non-existent publisher to a game
            Assert.That(result, Is.False);
        }
    }
}
