using GamingZoneApp.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;
using NUnit.Framework;

using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class DevelopersControllerTests
    {
        private Mock<IDeveloperService> developerServiceMock;
        private DevelopersController controller;

        [SetUp]
        public void SetUp()
        {
            developerServiceMock = new Mock<IDeveloperService>();

            controller = new DevelopersController(developerServiceMock.Object);

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
        public async Task Index_ReturnsViewWithPaginatedDevelopers()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.GetAllDevelopersWithInfoAsync())
                                .ReturnsAsync(new List<AllDevelopersViewModel>
                                {
                                    new AllDevelopersViewModel { Name = "TestDeveloperI" },
                                    new AllDevelopersViewModel { Name = "TestDeveloperII" }
                                });

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            developerServiceMock.Verify(ds => ds.GetAllDevelopersWithInfoAsync(), Times.Once);
        }

        [Test]
        public async Task Index_NoDevelopers_ReturnsViewWithEmptyList()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.GetAllDevelopersWithInfoAsync())
                                .ReturnsAsync(new List<AllDevelopersViewModel>());

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Index_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.GetAllDevelopersWithInfoAsync())
                                .ReturnsAsync(new List<AllDevelopersViewModel>());

            // Act
            IActionResult result = await controller.Index(2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeveloperGames_ReturnsViewWithPaginatedGames()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerServiceMock.Setup(ds => ds.GetAllGamesByDeveloperIdAsync(developerId))
                                .ReturnsAsync(new List<AllGamesViewModel>
                                {
                                    new AllGamesViewModel { Title = "TestGameI" },
                                    new AllGamesViewModel { Title = "TestGameII" }
                                });

            // Act
            IActionResult result = await controller.DeveloperGames(developerId, null);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            developerServiceMock.Verify(ds => ds.GetAllGamesByDeveloperIdAsync(developerId), Times.Once);
        }

        [Test]
        public async Task DeveloperGames_NoGames_ReturnsViewWithEmptyList()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerServiceMock.Setup(ds => ds.GetAllGamesByDeveloperIdAsync(developerId))
                                .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.DeveloperGames(developerId, null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeveloperGames_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            Guid developerId = Guid.NewGuid();

            developerServiceMock.Setup(ds => ds.GetAllGamesByDeveloperIdAsync(developerId))
                                .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.DeveloperGames(developerId, 2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
