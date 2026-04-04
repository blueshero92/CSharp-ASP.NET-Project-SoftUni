using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Publisher;
using GamingZoneApp.ViewModels.Publisher;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class PublisherManagementServiceTests
    {
        private Mock<IPublisherRepository> publisherRepositoryMock;
        private IPublisherManagementService publisherManagementService;

        private static Publisher CreateTestPublisher(Guid? id = null)
        {
            return new Publisher
            {
                Id = id ?? Guid.NewGuid(),
                Name = "TestPublisher",
                Description = "Test description",
                ImageUrl = "https://example.com/testpub.png"
            };
        }

        [SetUp]
        public void SetUp()
        {
            publisherRepositoryMock = new Mock<IPublisherRepository>();
            publisherManagementService = new PublisherManagementService(publisherRepositoryMock.Object);
        }

        [Test]
        public async Task AddPublisherAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            PublisherInputModel inputModel = new PublisherInputModel
            {
                Name = "TestPublisher",
                Description = "Test description",
                ImageUrl = "https://example.com/testpub.png"
            };

            publisherRepositoryMock.Setup(pr => pr.CreatePublisherAsync(It.IsAny<Publisher>()))
                                   .Returns(Task.CompletedTask);

            // Act
            bool result = await publisherManagementService.AddPublisherAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            publisherRepositoryMock.Verify(pr => pr.CreatePublisherAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [Test]
        public async Task AddPublisherAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            PublisherInputModel inputModel = new PublisherInputModel
            {
                Name = "TestPublisher",
                Description = "Test description"
            };

            publisherRepositoryMock.Setup(pr => pr.CreatePublisherAsync(It.IsAny<Publisher>()))
                                   .ThrowsAsync(new Exception());

            // Act
            bool result = await publisherManagementService.AddPublisherAsync(inputModel);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetPublisherForEditAsync_PublisherExists_ReturnsInputModel()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync(publisher);

            // Act
            PublisherInputModel? result = await publisherManagementService.GetPublisherForEditAsync(publisherId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestPublisher"));
            Assert.That(result.Description, Is.EqualTo("Test description"));
            Assert.That(result.ImageUrl, Is.EqualTo("https://example.com/testpub.png"));
        }

        [Test]
        public async Task GetPublisherForEditAsync_PublisherDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync((Publisher?)null);

            // Act
            PublisherInputModel? result = await publisherManagementService.GetPublisherForEditAsync(publisherId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditPublisherAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            PublisherInputModel inputModel = new PublisherInputModel
            {
                Name = "UpdatedPublisher",
                Description = "Updated description",
                ImageUrl = "https://example.com/updated.png"
            };

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId)).ReturnsAsync(publisher);
            publisherRepositoryMock.Setup(pr => pr.UpdatePublisherAsync(It.IsAny<Publisher>())).Returns(Task.CompletedTask);

            // Act
            bool result = await publisherManagementService.EditPublisherAsync(publisherId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            publisherRepositoryMock.Verify(pr => pr.UpdatePublisherAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [Test]
        public async Task EditPublisherAsync_PublisherDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync((Publisher?)null);

            // Act
            bool result = await publisherManagementService.EditPublisherAsync(publisherId, new PublisherInputModel());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditPublisherAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            PublisherInputModel inputModel = new PublisherInputModel
            {
                Name = "UpdatedPublisher",
                Description = "Updated description"
            };

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId)).ReturnsAsync(publisher);
            publisherRepositoryMock.Setup(pr => pr.UpdatePublisherAsync(It.IsAny<Publisher>())).ThrowsAsync(new Exception());

            // Act
            bool result = await publisherManagementService.EditPublisherAsync(publisherId, inputModel);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetPublisherForDeleteAsync_PublisherExists_ReturnsDeleteViewModel()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync(publisher);

            // Act
            DeletePublisherViewModel? result = await publisherManagementService.GetPublisherForDeleteAsync(publisherId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("TestPublisher"));
        }

        [Test]
        public async Task GetPublisherForDeleteAsync_PublisherDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync((Publisher?)null);

            // Act
            DeletePublisherViewModel? result = await publisherManagementService.GetPublisherForDeleteAsync(publisherId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task HardDeletePublisherAsync_PublisherExists_ReturnsTrue()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId)).ReturnsAsync(publisher);
            publisherRepositoryMock.Setup(pr => pr.DeletePublisherAsync(It.IsAny<Publisher>())).Returns(Task.CompletedTask);

            // Act
            bool result = await publisherManagementService.HardDeletePublisherAsync(publisherId);

            // Assert
            Assert.That(result, Is.True);
            publisherRepositoryMock.Verify(pr => pr.DeletePublisherAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [Test]
        public async Task HardDeletePublisherAsync_PublisherDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId))
                                   .ReturnsAsync((Publisher?)null);

            // Act
            bool result = await publisherManagementService.HardDeletePublisherAsync(publisherId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task HardDeletePublisherAsync_RepositoryThrows_ReturnsFalse()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();
            Publisher publisher = CreateTestPublisher(publisherId);

            publisherRepositoryMock.Setup(pr => pr.GetPublisherByIdAsync(publisherId)).ReturnsAsync(publisher);
            publisherRepositoryMock.Setup(pr => pr.DeletePublisherAsync(It.IsAny<Publisher>())).ThrowsAsync(new Exception());

            // Act
            bool result = await publisherManagementService.HardDeletePublisherAsync(publisherId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}