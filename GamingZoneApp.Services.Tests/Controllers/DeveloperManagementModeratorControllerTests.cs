using GamingZoneApp.Areas.Moderator.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;
using NUnit.Framework;

using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class DeveloperManagementModeratorControllerTests
    {
        private Mock<IDeveloperService> developerServiceMock;
        private Mock<IDeveloperManagementService> developerManagementServiceMock;
        private DeveloperManagementModeratorController controller;

        private static readonly Guid TestDeveloperId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            developerServiceMock = new Mock<IDeveloperService>();
            developerManagementServiceMock = new Mock<IDeveloperManagementService>();

            controller = new DeveloperManagementModeratorController(
                developerServiceMock.Object,
                developerManagementServiceMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "Moderator")
                    }, "TestAuth"))
                }
            };

            controller.TempData = new TempDataDictionary(
                controller.HttpContext,
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Index_ReturnsViewWithAllDevelopers()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.GetAllDevelopersWithInfoAsync())
                                .ReturnsAsync(new List<AllDevelopersViewModel>());

            // Act
            IActionResult result = await controller.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            developerServiceMock.Verify(ds => ds.GetAllDevelopersWithInfoAsync(), Times.Once);
        }

        [Test]
        public void AddDeveloper_Get_ReturnsView()
        {
            // Act
            IActionResult result = controller.AddDeveloper();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddDeveloper_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            IActionResult result = await controller.AddDeveloper(new DeveloperInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddDeveloper_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            developerManagementServiceMock.Setup(dms => dms.AddDeveloperAsync(It.IsAny<DeveloperInputModel>()))
                                          .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AddDeveloper(new DeveloperInputModel { Name = "TestDev", Description = "Desc" });

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddDeveloper_Post_Success_RedirectsToIndex()
        {
            // Arrange
            developerManagementServiceMock.Setup(dms => dms.AddDeveloperAsync(It.IsAny<DeveloperInputModel>()))
                                          .ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddDeveloper(new DeveloperInputModel { Name = "TestDev", Description = "Desc" });

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task EditDeveloper_Get_DeveloperDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task EditDeveloper_Get_ServiceReturnsNull_RedirectsToIndex()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.GetDeveloperForEditAsync(TestDeveloperId))
                                          .ReturnsAsync((DeveloperInputModel?)null);

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task EditDeveloper_Get_Success_ReturnsView()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.GetDeveloperForEditAsync(TestDeveloperId))
                                          .ReturnsAsync(new DeveloperInputModel { Name = "TestDev" });

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((DeveloperInputModel)viewResult!.Model!).Name, Is.EqualTo("TestDev"));
        }

        [Test]
        public async Task EditDeveloper_Post_DeveloperDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId, new DeveloperInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task EditDeveloper_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId, new DeveloperInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task EditDeveloper_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.EditDeveloperAsync(TestDeveloperId, It.IsAny<DeveloperInputModel>()))
                                          .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId, new DeveloperInputModel { Name = "TestDev", Description = "Desc" });

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task EditDeveloper_Post_Success_RedirectsToIndex()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.EditDeveloperAsync(TestDeveloperId, It.IsAny<DeveloperInputModel>()))
                                          .ReturnsAsync(true);

            // Act
            IActionResult result = await controller.EditDeveloper(TestDeveloperId, new DeveloperInputModel { Name = "TestDev", Description = "Desc" });

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteDeveloper_Get_DeveloperDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteDeveloper_Get_ServiceReturnsNull_RedirectsToIndex()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.GetDeveloperForDeleteAsync(TestDeveloperId))
                                          .ReturnsAsync((DeleteDeveloperViewModel?)null);

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteDeveloper_Get_Success_ReturnsView()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.GetDeveloperForDeleteAsync(TestDeveloperId))
                                          .ReturnsAsync(new DeleteDeveloperViewModel { Name = "TestDev" });

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeleteDeveloper_Post_DeveloperDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId, new DeleteDeveloperViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteDeveloper_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.HardDeleteDeveloperAsync(TestDeveloperId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId, new DeleteDeveloperViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeleteDeveloper_Post_Success_RedirectsToIndex()
        {
            // Arrange
            developerServiceMock.Setup(ds => ds.DeveloperExistsAsync(TestDeveloperId)).ReturnsAsync(true);
            developerManagementServiceMock.Setup(dms => dms.HardDeleteDeveloperAsync(TestDeveloperId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.DeleteDeveloper(TestDeveloperId, new DeleteDeveloperViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }
    }
}
