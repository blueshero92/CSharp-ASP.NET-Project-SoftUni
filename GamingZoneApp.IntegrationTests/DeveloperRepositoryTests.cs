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
    public class DeveloperRepositoryTests
    {
        private Mock<GamingZoneDbContext> dbContextMock;
        private IDeveloperRepository developerRepository;

        private static readonly Guid TestDevId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<GamingZoneDbContext> options =
                new DbContextOptionsBuilder<GamingZoneDbContext>().Options;

            dbContextMock = new Mock<GamingZoneDbContext>(options);
            developerRepository = new DeveloperRepository(dbContextMock.Object);
        }

        [Test]
        public async Task GetAllDevelopersNoTracking_ReturnsDevelopers()
        {
            // Arrange
            List<Developer> developers = new List<Developer>
            {
                new Developer { Id = TestDevId, Name = "Dev1" },
                new Developer { Id = Guid.NewGuid(), Name = "Dev2" }
            };

            dbContextMock.Setup(c => c.Developers).Returns(developers.BuildMockDbSet().Object);

            // Act
            List<Developer> result = await developerRepository.GetAllDevelopersNoTracking().ToListAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetAllGamesByDeveloperNoTracking_FiltersByDeveloperId()
        {
            // Arrange
            List<Game> games = new List<Game>
            {
                new Game { Id = Guid.NewGuid(), DeveloperId = TestDevId, Developer = new Developer(), Publisher = new Publisher() },
                new Game { Id = Guid.NewGuid(), DeveloperId = Guid.NewGuid(), Developer = new Developer(), Publisher = new Publisher() }
            };

            dbContextMock.Setup(c => c.Games).Returns(games.BuildMockDbSet().Object);

            // Act
            List<Game> result = await developerRepository.GetAllGamesByDeveloperNoTracking(TestDevId).ToListAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].DeveloperId, Is.EqualTo(TestDevId));
        }

        [Test]
        public async Task CheckIfDeveloperExistsAsync_Exists_ReturnsTrue()
        {
            // Arrange
            List<Developer> developers = new List<Developer> { new Developer { Id = TestDevId } };
            dbContextMock.Setup(c => c.Developers).Returns(developers.BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await developerRepository.CheckIfDeveloperExistsAsync(TestDevId), Is.True);
        }

        [Test]
        public async Task CheckIfDeveloperExistsAsync_DoesNotExist_ReturnsFalse()
        {
            // Arrange
            dbContextMock.Setup(c => c.Developers).Returns(new List<Developer>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await developerRepository.CheckIfDeveloperExistsAsync(Guid.NewGuid()), Is.False);
        }

        [Test]
        public async Task GetDeveloperByIdAsync_Exists_ReturnsDeveloper()
        {
            // Arrange
            List<Developer> developers = new List<Developer>
            {
                new Developer { Id = TestDevId, Name = "TestDev" }
            };

            dbContextMock.Setup(c => c.Developers).Returns(developers.BuildMockDbSet().Object);

            // Act
            Developer? result = await developerRepository.GetDeveloperByIdAsync(TestDevId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestDev"));
        }

        [Test]
        public async Task GetDeveloperByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            dbContextMock.Setup(c => c.Developers).Returns(new List<Developer>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await developerRepository.GetDeveloperByIdAsync(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public async Task CreateDeveloperAsync_AddsAndSaves()
        {
            // Arrange
            Mock<DbSet<Developer>> mockDbSet = new List<Developer>().BuildMockDbSet();
            dbContextMock.Setup(c => c.Developers).Returns(mockDbSet.Object);

            Developer developer = new Developer { Id = Guid.NewGuid(), Name = "New" };

            // Act
            await developerRepository.CreateDeveloperAsync(developer);

            // Assert
            mockDbSet.Verify(d => d.AddAsync(developer, It.IsAny<CancellationToken>()), Times.Once);
            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdateDeveloperAsync_UpdatesAndSaves()
        {
            // Arrange
            Developer developer = new Developer { Id = TestDevId, Name = "Updated" };
            Mock<DbSet<Developer>> mockDbSet = new List<Developer> { developer }.BuildMockDbSet();
            dbContextMock.Setup(c => c.Developers).Returns(mockDbSet.Object);

            // Act
            await developerRepository.UpdateDeveloperAsync(developer);

            // Assert
            mockDbSet.Verify(d => d.Update(developer), Times.Once);
            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteDeveloperAsync_RemovesAndSaves()
        {
            // Arrange
            Developer developer = new Developer { Id = TestDevId, Name = "ToDelete" };
            Mock<DbSet<Developer>> mockDbSet = new List<Developer> { developer }.BuildMockDbSet();
            dbContextMock.Setup(c => c.Developers).Returns(mockDbSet.Object);

            // Act
            await developerRepository.DeleteDeveloperAsync(developer);

            // Assert
            mockDbSet.Verify(d => d.Remove(developer), Times.Once);
            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}