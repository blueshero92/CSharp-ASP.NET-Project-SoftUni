using GamingZoneApp.Areas.Admin.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class GameManagementControllerTests
    {
        private Mock<IGameService> gameServiceMock;
        private Mock<IGameManagementService> gameManagementServiceMock;
        private Mock<IDeveloperService> developerServiceMock;
        private Mock<IPublisherService> publisherServiceMock;
        private GameManagementController controller;

        private static readonly Guid TestGameId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            gameServiceMock = new Mock<IGameService>();
            gameManagementServiceMock = new Mock<IGameManagementService>();
            developerServiceMock = new Mock<IDeveloperService>();
            publisherServiceMock = new Mock<IPublisherService>();

            controller = new GameManagementController(
                gameServiceMock.Object,
                gameManagementServiceMock.Object,
                developerServiceMock.Object,
                publisherServiceMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, "TestAuth"))
                }
            };

            controller.TempData = new TempDataDictionary(
                controller.HttpContext,
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Index_ReturnsViewWithAllGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetAllGamesAsync())
                           .ReturnsAsync(new List<AllGamesViewModel>
                           {
                               new AllGamesViewModel { Title = "TestGameI" }
                           });

            // Act
            IActionResult result = await controller.Index();

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            gameServiceMock.Verify(gs => gs.GetAllGamesAsync(), Times.Once);
        }

        [Test]
        public async Task Edit_Get_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Edit(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Edit_Get_ServiceReturnsNull_RedirectsToEdit()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.GetEditAsync(TestGameId))
                                     .ReturnsAsync((GameInputModel?)null);

            // Act
            IActionResult result = await controller.Edit(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("Edit"));
        }

        [Test]
        public async Task Edit_Get_GameFound_ReturnsViewWithInputModel()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.GetEditAsync(TestGameId))
                                     .ReturnsAsync(new GameInputModel { Title = "TestGame" });
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync())
                                .ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync())
                                .ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.Edit(TestGameId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((GameInputModel)viewResult!.Model!).Title, Is.EqualTo("TestGame"));
        }

        [Test]
        public async Task Edit_Post_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Edit(TestGameId, new GameInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            controller.ModelState.AddModelError("Title", "Required");

            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync())
                                .ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync())
                                .ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.Edit(TestGameId, new GameInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Edit_Post_DeveloperDoesNotExist_ReturnsView()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "Adventure",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.Edit(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Edit_Post_PublisherDoesNotExist_ReturnsView()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "Adventure",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.Edit(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Edit_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "Adventure",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.PostEditAsync(TestGameId, inputModel)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Edit(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Edit_Post_Success_RedirectsToIndex()
        {
            // Arrange
            GameInputModel inputModel = new GameInputModel
            {
                Title = "TestGame",
                Genre = "Adventure",
                Description = "Desc",
                DeveloperId = Guid.NewGuid(),
                PublisherId = Guid.NewGuid()
            };

            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.PostEditAsync(TestGameId, inputModel)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.Edit(TestGameId, inputModel);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Delete_Get_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Delete(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Delete_Get_ServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.GetDeleteAsync(TestGameId))
                                     .ReturnsAsync((DeleteGameViewModel?)null);

            // Act
            IActionResult result = await controller.Delete(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_GameFound_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.GetDeleteAsync(TestGameId))
                                     .ReturnsAsync(new DeleteGameViewModel { Title = "TestGame" });

            // Act
            IActionResult result = await controller.Delete(TestGameId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Post_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Delete(TestGameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Delete_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.PostDeleteAsync(TestGameId, It.IsAny<DeleteGameViewModel>()))
                                     .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.Delete(TestGameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Delete_Post_Success_RedirectsToIndex()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameManagementServiceMock.Setup(gms => gms.PostDeleteAsync(TestGameId, It.IsAny<DeleteGameViewModel>()))
                                     .ReturnsAsync(true);

            // Act
            IActionResult result = await controller.Delete(TestGameId, new DeleteGameViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }
    }
}
