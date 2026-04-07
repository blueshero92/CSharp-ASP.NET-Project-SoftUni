using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository;
using GamingZoneApp.Data.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.IntegrationTests
{
    [TestFixture]
    public class PublisherRepositoryTests
    {
        private Mock<GamingZoneDbContext> dbContextMock;
        private IPublisherRepository publisherRepository;

        private static readonly Guid TestPubId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<GamingZoneDbContext> options =
                new DbContextOptionsBuilder<GamingZoneDbContext>().Options;

            dbContextMock = new Mock<GamingZoneDbContext>(options);
            publisherRepository = new PublisherRepository(dbContextMock.Object);
        }

        [Test]
        public async Task GetAllPublishersNoTracking_ReturnsPublishers()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher { Id = TestPubId, Name = "Pub1" },
                new Publisher { Id = Guid.NewGuid(), Name = "Pub2" }
            };

            dbContextMock.Setup(c => c.Publishers).Returns(publishers.BuildMockDbSet().Object);

            // Act
            List<Publisher> result = await publisherRepository.GetAllPublishersNoTracking().ToListAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetAllGamesByPublisherNoTracking_FiltersByPublisherId()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game { Id = Guid.NewGuid(), PublisherId = TestPubId, Developer = new Developer(), Publisher = new Publisher() },
                new Game { Id = Guid.NewGuid(), PublisherId = Guid.NewGuid(), Developer = new Developer(), Publisher = new Publisher() }
            };

            dbContextMock.Setup(c => c.Games).Returns(games.BuildMockDbSet().Object);

            // Act
            List<Game> result = await publisherRepository.GetAllGamesByPublisherNoTracking(TestPubId).ToListAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].PublisherId, Is.EqualTo(TestPubId));
        }

        [Test]
        public async Task CheckIfPublisherExistsAsync_Exists_ReturnsTrue()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher> { new Publisher { Id = TestPubId } };

            dbContextMock.Setup(c => c.Publishers).Returns(publishers.BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await publisherRepository.CheckIfPublisherExistsAsync(TestPubId), Is.True);
        }

        [Test]
        public async Task CheckIfPublisherExistsAsync_DoesNotExist_ReturnsFalse()
        {
            // Arrange
            dbContextMock.Setup(c => c.Publishers).Returns(new List<Publisher>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await publisherRepository.CheckIfPublisherExistsAsync(Guid.NewGuid()), Is.False);
        }

        [Test]
        public async Task GetPublisherByIdAsync_Exists_ReturnsPublisher()
        {
            // Arrange
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher { Id = TestPubId, Name = "TestPub" }
            };

            dbContextMock.Setup(c => c.Publishers).Returns(publishers.BuildMockDbSet().Object);

            // Act
            Publisher? result = await publisherRepository.GetPublisherByIdAsync(TestPubId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestPub"));
        }

        [Test]
        public async Task GetPublisherByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            dbContextMock.Setup(c => c.Publishers).Returns(new List<Publisher>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await publisherRepository.GetPublisherByIdAsync(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public async Task CreatePublisherAsync_AddsAndSaves()
        {
            // Arrange
            Mock<DbSet<Publisher>> mockDbSet = new List<Publisher>().BuildMockDbSet();

            dbContextMock.Setup(c => c.Publishers).Returns(mockDbSet.Object);

            Publisher publisher = new Publisher { Id = Guid.NewGuid(), Name = "New" };

            // Act
            await publisherRepository.CreatePublisherAsync(publisher);

            // Assert
            mockDbSet.Verify(d => d.Add(publisher), Times.Once);
            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdatePublisherAsync_UpdatesAndSaves()
        {
            // Arrange
            Publisher publisher = new Publisher { Id = TestPubId, Name = "Updated" };

            Mock<DbSet<Publisher>> mockDbSet = new List<Publisher> { publisher }.BuildMockDbSet();

            dbContextMock.Setup(c => c.Publishers).Returns(mockDbSet.Object);

            // Act
            await publisherRepository.UpdatePublisherAsync(publisher);

            // Assert
            mockDbSet.Verify(d => d.Update(publisher), Times.Once);

            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeletePublisherAsync_RemovesAndSaves()
        {
            // Arrange
            Publisher publisher = new Publisher { Id = TestPubId, Name = "ToDelete" };

            Mock<DbSet<Publisher>> mockDbSet = new List<Publisher> { publisher }.BuildMockDbSet();

            dbContextMock.Setup(c => c.Publishers).Returns(mockDbSet.Object);

            // Act
            await publisherRepository.DeletePublisherAsync(publisher);

            // Assert
            mockDbSet.Verify(d => d.Remove(publisher), Times.Once);

            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}