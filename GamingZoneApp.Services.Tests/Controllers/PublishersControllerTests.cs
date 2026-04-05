using GamingZoneApp.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;
using NUnit.Framework;

using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class PublishersControllerTests
    {
        private Mock<IPublisherService> publisherServiceMock;
        private PublishersController controller;

        [SetUp]
        public void SetUp()
        {
            publisherServiceMock = new Mock<IPublisherService>();

            controller = new PublishersController(publisherServiceMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                    }, "TestAuth"))
                }
            };
        }

        [Test]
        public async Task Index_ReturnsViewWithPaginatedPublishers()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.GetAllPublishersWithInfoAsync())
                                .ReturnsAsync(new List<AllPublishersViewModel>
                                {
                                    new AllPublishersViewModel { Name = "TestPublisherI" },
                                    new AllPublishersViewModel { Name = "TestPublisherII" }
                                });

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            publisherServiceMock.Verify(ps => ps.GetAllPublishersWithInfoAsync(), Times.Once);
        }

        [Test]
        public async Task Index_NoPublishers_ReturnsViewWithEmptyList()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.GetAllPublishersWithInfoAsync())
                                .ReturnsAsync(new List<AllPublishersViewModel>());

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Index_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.GetAllPublishersWithInfoAsync())
                                .ReturnsAsync(new List<AllPublishersViewModel>());

            // Act
            IActionResult result = await controller.Index(2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PublisherGames_ReturnsViewWithPaginatedGames()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherServiceMock.Setup(ps => ps.GetAllGamesByPublisherIdAsync(publisherId))
                                .ReturnsAsync(new List<AllGamesViewModel>
                                {
                                    new AllGamesViewModel { Title = "TestGameI" },
                                    new AllGamesViewModel { Title = "TestGameII" }
                                });

            // Act
            IActionResult result = await controller.PublisherGames(publisherId, null);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            publisherServiceMock.Verify(ps => ps.GetAllGamesByPublisherIdAsync(publisherId), Times.Once);
        }

        [Test]
        public async Task PublisherGames_NoGames_ReturnsViewWithEmptyList()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherServiceMock.Setup(ps => ps.GetAllGamesByPublisherIdAsync(publisherId))
                                .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.PublisherGames(publisherId, null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PublisherGames_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            Guid publisherId = Guid.NewGuid();

            publisherServiceMock.Setup(ps => ps.GetAllGamesByPublisherIdAsync(publisherId))
                                .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.PublisherGames(publisherId, 2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
