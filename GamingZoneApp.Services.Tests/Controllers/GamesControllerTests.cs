using GamingZoneApp.Controllers;
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
    public class GamesControllerTests
    {
        private Mock<IGameService> gameServiceMock;
        private Mock<IDeveloperService> developerServiceMock;
        private Mock<IPublisherService> publisherServiceMock;
        private GamesController controller;

        private static readonly Guid TestUserId = Guid.NewGuid();
        private static readonly Guid TestGameId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            gameServiceMock = new Mock<IGameService>();
            developerServiceMock = new Mock<IDeveloperService>();
            publisherServiceMock = new Mock<IPublisherService>();

            controller = new GamesController(
                gameServiceMock.Object,
                developerServiceMock.Object,
                publisherServiceMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, TestUserId.ToString())
                    }, "TestAuth"))
                }
            };

            controller.TempData = new TempDataDictionary(
                controller.HttpContext,
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Index_NoSearchQuery_ReturnsViewWithAllGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetAllGamesAsync())
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.Index(null, null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            gameServiceMock.Verify(gs => gs.GetAllGamesAsync(), Times.Once);
        }

        [Test]
        public async Task Index_WithSearchQuery_ReturnsViewWithFilteredGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.SearchGamesAsync("TestQuery"))
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.Index("TestQuery", null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            gameServiceMock.Verify(gs => gs.SearchGamesAsync("TestQuery"), Times.Once);
        }


        [Test]
        public async Task GameDetails_GameExists_ReturnsViewResult()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetGameDetailsByIdAsync(TestGameId))
                           .ReturnsAsync(new GameViewModel { Title = "TestGame" });

            // Act
            IActionResult result = await controller.GameDetails(TestGameId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((GameViewModel)viewResult!.Model!).Title, Is.EqualTo("TestGame"));
        }

        [Test]
        public async Task GameDetails_GameDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetGameDetailsByIdAsync(TestGameId))
                           .ReturnsAsync((GameViewModel?)null);

            // Act
            IActionResult result = await controller.GameDetails(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }


        [Test]
        public async Task AddToFavorites_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId))
                           .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AddToFavorites(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task AddToFavorites_GameAlreadyInFavorites_RedirectsWithError()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddToFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }

        [Test]
        public async Task AddToFavorites_UserIsCreator_RedirectsWithError()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddToFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }

        [Test]
        public async Task AddToFavorites_Success_RedirectsToMyFavoriteGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.AddGameToFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddToFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }

        [Test]
        public async Task AddToFavorites_ServiceFails_RedirectsToGameDetails()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.AddGameToFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AddToFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("GameDetails"));
        }


        [Test]
        public async Task RemoveFromFavorites_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.RemoveFromFavorites(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task RemoveFromFavorites_NotInFavorites_UserIsCreator_RedirectsWithError()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.RemoveFromFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }

        [Test]
        public async Task RemoveFromFavorites_NotInFavorites_NotCreator_RedirectsWithError()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.RemoveFromFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }

        [Test]
        public async Task RemoveFromFavorites_Success_RedirectsToMyFavoriteGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.RemoveGameFromFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.RemoveFromFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("MyFavoriteGames"));
        }


        [Test]
        public async Task AddGame_Get_ReturnsViewWithDevelopersAndPublishers()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync())
                                .ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync())
                                .ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.AddGame();

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<GameInputModel>());
        }


        [Test]
        public async Task AddGame_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            controller.ModelState.AddModelError("Title", "Required");

            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync())
                                .ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync())
                                .ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.AddGame(new GameInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddGame_Post_DeveloperDoesNotExist_ReturnsView()
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

            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.AddGame(inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddGame_Post_PublisherDoesNotExist_ReturnsView()
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

            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.AddGame(inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddGame_Post_Success_RedirectsToMyGames()
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

            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.AddGameAsync(inputModel, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddGame(inputModel);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("MyGames"));
        }


        [Test]
        public async Task EditGame_Get_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditGame(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task EditGame_Get_NotCreator_Returns403()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditGame(TestGameId);

            // Assert
            StatusCodeResult statusCode = result as StatusCodeResult;
            Assert.That(statusCode!.StatusCode, Is.EqualTo(403));
        }

        [Test]
        public async Task EditGame_Get_GameFound_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.GetGameForEditAsync(TestGameId, TestUserId))
                           .ReturnsAsync(new GameInputModel { Title = "TestGame" });
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.EditGame(TestGameId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((GameInputModel)viewResult!.Model!).Title, Is.EqualTo("TestGame"));
        }

        [Test]
        public async Task EditGame_Post_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditGame(TestGameId, new GameInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task EditGame_Post_NotCreator_Returns403()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditGame(TestGameId, new GameInputModel());

            // Assert
            StatusCodeResult statusCode = result as StatusCodeResult;
            Assert.That(statusCode!.StatusCode, Is.EqualTo(403));
        }

        [Test]
        public async Task EditGame_Post_Success_RedirectsToIndex()
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
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.EditGameAsync(TestGameId, inputModel, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.EditGame(TestGameId, inputModel);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }


        [Test]
        public async Task DeleteGame_Get_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task DeleteGame_Get_NotCreator_Returns403()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId);

            // Assert
            StatusCodeResult statusCode = result as StatusCodeResult;
            Assert.That(statusCode!.StatusCode, Is.EqualTo(403));
        }

        [Test]
        public async Task DeleteGame_Get_GameFound_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.GetGameForDeleteAsync(TestGameId, TestUserId))
                           .ReturnsAsync(new DeleteGameViewModel { Title = "TestGame" });

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
        }

        [Test]
        public async Task DeleteGame_Get_ServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.GetGameForDeleteAsync(TestGameId, TestUserId))
                           .ReturnsAsync((DeleteGameViewModel?)null);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteGame_Post_GameDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task DeleteGame_Post_NotCreator_Returns403()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId, new DeleteGameViewModel());

            // Assert
            StatusCodeResult statusCode = result as StatusCodeResult;
            Assert.That(statusCode!.StatusCode, Is.EqualTo(403));
        }

        [Test]
        public async Task DeleteGame_Post_Success_RedirectsToIndex()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.SoftDeleteGameAsync(TestGameId, TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId, new DeleteGameViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteGame_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.SoftDeleteGameAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteGame(TestGameId, new DeleteGameViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }


        [Test]
        public async Task MyGames_ReturnsViewWithPaginatedGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetAllGamesByUserIdAsync(TestUserId))
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.MyGames(null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            gameServiceMock.Verify(gs => gs.GetAllGamesByUserIdAsync(TestUserId), Times.Once);
        }

        [Test]
        public async Task MyGames_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetAllGamesByUserIdAsync(TestUserId))
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.MyGames(2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== MyFavoriteGames =====

        [Test]
        public async Task MyFavoriteGames_ReturnsViewWithPaginatedGames()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetFavoriteGamesByUserIdAsync(TestUserId))
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.MyFavoriteGames(null);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            gameServiceMock.Verify(gs => gs.GetFavoriteGamesByUserIdAsync(TestUserId), Times.Once);
        }

        [Test]
        public async Task MyFavoriteGames_WithPageNumber_ReturnsViewResult()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GetFavoriteGamesByUserIdAsync(TestUserId))
                           .ReturnsAsync(new List<AllGamesViewModel>());

            // Act
            IActionResult result = await controller.MyFavoriteGames(2);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== RemoveFromFavorites – service fails =====

        [Test]
        public async Task RemoveFromFavorites_ServiceFails_RedirectsToGameDetails()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsGameInFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.RemoveGameFromFavoritesAsync(TestGameId, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.RemoveFromFavorites(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("GameDetails"));
        }

        // ===== AddGame POST – service fails =====

        [Test]
        public async Task AddGame_Post_ServiceFails_ReturnsView()
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

            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.AddGameAsync(inputModel, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AddGame(inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== EditGame GET – service returns null =====

        [Test]
        public async Task EditGame_Get_ServiceReturnsNull_RedirectsToEditGame()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.GetGameForEditAsync(TestGameId, TestUserId))
                           .ReturnsAsync((GameInputModel?)null);

            // Act
            IActionResult result = await controller.EditGame(TestGameId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect!.ActionName, Is.EqualTo("EditGame"));
        }

        // ===== EditGame POST – invalid model =====

        [Test]
        public async Task EditGame_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            gameServiceMock.Setup(gs => gs.GameExistsAsync(TestGameId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            controller.ModelState.AddModelError("Title", "Required");

            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.EditGame(TestGameId, new GameInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== EditGame POST – developer doesn't exist =====

        [Test]
        public async Task EditGame_Post_DeveloperDoesNotExist_ReturnsView()
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
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.EditGame(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== EditGame POST – publisher doesn't exist =====

        [Test]
        public async Task EditGame_Post_PublisherDoesNotExist_ReturnsView()
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
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(false);
            developerServiceMock.Setup(ds => ds.GetAllDevelopersAsync()).ReturnsAsync(new List<AddGameDeveloperViewModel>());
            publisherServiceMock.Setup(ps => ps.GetAllPublishersAsync()).ReturnsAsync(new List<AddGamePublisherViewModel>());

            // Act
            IActionResult result = await controller.EditGame(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        // ===== EditGame POST – service fails =====

        [Test]
        public async Task EditGame_Post_ServiceFails_ReturnsView()
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
            gameServiceMock.Setup(gs => gs.IsUserCreatorAsync(TestGameId, TestUserId)).ReturnsAsync(true);
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(inputModel.DeveloperId)).ReturnsAsync(true);
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(inputModel.PublisherId)).ReturnsAsync(true);
            gameServiceMock.Setup(gs => gs.EditGameAsync(TestGameId, inputModel, TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditGame(TestGameId, inputModel);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
