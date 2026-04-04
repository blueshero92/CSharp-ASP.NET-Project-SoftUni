using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class DeveloperManagementServiceTests
    {
        private Mock<IDeveloperRepository> developerRepositoryMock;
        private IDeveloperManagementService developerManagementService;

        private static Developer CreateTestDeveloper(Guid? id = null)
        {
            return new Developer
            {
                Id = id ?? Guid.NewGuid(),
                Name = "TestDeveloper",
                Description = "Test description",
                ImageUrl = "https://example.com/testdev.png"
            };
        }

        [SetUp]
        public void SetUp()
        {
            developerRepositoryMock = new Mock<IDeveloperRepository>();
            developerManagementService = new DeveloperManagementService(developerRepositoryMock.Object);
        }

        [Test]
        public async Task AddDeveloperAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            DeveloperInputModel inputModel = new DeveloperInputModel
            {
                Name = "TestDeveloper",
                Description = "Test description",
                ImageUrl = "https://example.com/testdev.png"
            };

            developerRepositoryMock.Setup(dr => dr.CreateDeveloperAsync(It.IsAny<Developer>()))
                                   .Returns(Task.CompletedTask);

            // Act
            bool result = await developerManagementService.AddDeveloperAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            developerRepositoryMock.Verify(dr => dr.CreateDeveloperAsync(It.IsAny<Developer>()), Times.Once);
        }

        [Test]
        public async Task AddDeveloperAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            DeveloperInputModel inputModel = new DeveloperInputModel
            {
                Name = "TestDeveloper",
                Description = "Test description"
            };

            developerRepositoryMock.Setup(dr => dr.CreateDeveloperAsync(It.IsAny<Developer>()))
                                   .ThrowsAsync(new Exception());

            // Act
            bool result = await developerManagementService.AddDeveloperAsync(inputModel);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetDeveloperForEditAsync_DeveloperExists_ReturnsInputModel()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync(developer);

            // Act
            DeveloperInputModel? result = await developerManagementService.GetDeveloperForEditAsync(developerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestDeveloper"));
            Assert.That(result.Description, Is.EqualTo("Test description"));
            Assert.That(result.ImageUrl, Is.EqualTo("https://example.com/testdev.png"));
        }

        [Test]
        public async Task GetDeveloperForEditAsync_DeveloperDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync((Developer?)null);

            // Act
            DeveloperInputModel? result = await developerManagementService.GetDeveloperForEditAsync(developerId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditDeveloperAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            DeveloperInputModel inputModel = new DeveloperInputModel
            {
                Name = "UpdatedDeveloper",
                Description = "Updated description",
                ImageUrl = "https://example.com/updated.png"
            };

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId)).ReturnsAsync(developer);
            developerRepositoryMock.Setup(dr => dr.UpdateDeveloperAsync(It.IsAny<Developer>())).Returns(Task.CompletedTask);

            // Act
            bool result = await developerManagementService.EditDeveloperAsync(developerId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            developerRepositoryMock.Verify(dr => dr.UpdateDeveloperAsync(It.IsAny<Developer>()), Times.Once);
        }

        [Test]
        public async Task EditDeveloperAsync_DeveloperDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync((Developer?)null);

            // Act
            bool result = await developerManagementService.EditDeveloperAsync(developerId, new DeveloperInputModel());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditDeveloperAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            DeveloperInputModel inputModel = new DeveloperInputModel
            {
                Name = "UpdatedDeveloper",
                Description = "Updated description"
            };

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId)).ReturnsAsync(developer);
            developerRepositoryMock.Setup(dr => dr.UpdateDeveloperAsync(It.IsAny<Developer>())).ThrowsAsync(new Exception());

            // Act
            bool result = await developerManagementService.EditDeveloperAsync(developerId, inputModel);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetDeveloperForDeleteAsync_DeveloperExists_ReturnsDeleteViewModel()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync(developer);

            // Act
            DeleteDeveloperViewModel? result = await developerManagementService.GetDeveloperForDeleteAsync(developerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestDeveloper"));
        }

        [Test]
        public async Task GetDeveloperForDeleteAsync_DeveloperDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync((Developer?)null);

            // Act
            DeleteDeveloperViewModel? result = await developerManagementService.GetDeveloperForDeleteAsync(developerId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task HardDeleteDeveloperAsync_DeveloperExists_ReturnsTrue()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId)).ReturnsAsync(developer);
            developerRepositoryMock.Setup(dr => dr.DeleteDeveloperAsync(It.IsAny<Developer>())).Returns(Task.CompletedTask);

            // Act
            bool result = await developerManagementService.HardDeleteDeveloperAsync(developerId);

            // Assert
            Assert.That(result, Is.True);
            developerRepositoryMock.Verify(dr => dr.DeleteDeveloperAsync(It.IsAny<Developer>()), Times.Once);
        }

        [Test]
        public async Task HardDeleteDeveloperAsync_DeveloperDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId))
                                   .ReturnsAsync((Developer?)null);

            // Act
            bool result = await developerManagementService.HardDeleteDeveloperAsync(developerId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task HardDeleteDeveloperAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();
            Developer developer = CreateTestDeveloper(developerId);

            developerRepositoryMock.Setup(dr => dr.GetDeveloperByIdAsync(developerId)).ReturnsAsync(developer);
            developerRepositoryMock.Setup(dr => dr.DeleteDeveloperAsync(It.IsAny<Developer>())).ThrowsAsync(new Exception());

            // Act
            bool result = await developerManagementService.HardDeleteDeveloperAsync(developerId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}