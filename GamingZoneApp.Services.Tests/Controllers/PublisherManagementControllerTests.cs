using GamingZoneApp.Areas.Admin.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Publisher;
using GamingZoneApp.ViewModels.Publisher;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class PublisherManagementControllerTests
    {
        private Mock<IPublisherService> publisherServiceMock;
        private Mock<IPublisherManagementService> publisherManagementServiceMock;
        private PublisherManagementController controller;

        private static readonly Guid TestPublisherId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            publisherServiceMock = new Mock<IPublisherService>();
            publisherManagementServiceMock = new Mock<IPublisherManagementService>();

            controller = new PublisherManagementController(
                publisherServiceMock.Object,
                publisherManagementServiceMock.Object);

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
        public async Task Index_ReturnsViewWithAllPublishers()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.GetAllPublishersWithInfoAsync())
                                .ReturnsAsync(new List<AllPublishersViewModel>());

            // Act
            IActionResult result = await controller.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            publisherServiceMock.Verify(ps => ps.GetAllPublishersWithInfoAsync(), Times.Once);
        }

        [Test]
        public void AddPublisher_Get_ReturnsView()
        {
            // Act
            IActionResult result = controller.AddPublisher();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddPublisher_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            IActionResult result = await controller.AddPublisher(new PublisherInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddPublisher_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            publisherManagementServiceMock.Setup(pms => pms.AddPublisherAsync(It.IsAny<PublisherInputModel>()))
                                          .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AddPublisher(new PublisherInputModel { Name = "TestPub", Description = "Desc" });

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task AddPublisher_Post_Success_RedirectsToIndex()
        {
            // Arrange
            publisherManagementServiceMock.Setup(pms => pms.AddPublisherAsync(It.IsAny<PublisherInputModel>()))
                                          .ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AddPublisher(new PublisherInputModel { Name = "TestPub", Description = "Desc" });

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task EditPublisher_Get_PublisherDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task EditPublisher_Get_ServiceReturnsNull_RedirectsToIndex()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.GetPublisherForEditAsync(TestPublisherId))
                                          .ReturnsAsync((PublisherInputModel?)null);

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task EditPublisher_Get_Success_ReturnsView()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.GetPublisherForEditAsync(TestPublisherId))
                                          .ReturnsAsync(new PublisherInputModel { Name = "TestPub" });

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((PublisherInputModel)viewResult!.Model!).Name, Is.EqualTo("TestPub"));
        }

        [Test]
        public async Task EditPublisher_Post_PublisherDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId, new PublisherInputModel());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task EditPublisher_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.EditPublisherAsync(TestPublisherId, It.IsAny<PublisherInputModel>()))
                                          .ReturnsAsync(false);

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId, new PublisherInputModel { Name = "TestPub", Description = "Desc" });

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task EditPublisher_Post_Success_RedirectsToIndex()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.EditPublisherAsync(TestPublisherId, It.IsAny<PublisherInputModel>()))
                                          .ReturnsAsync(true);

            // Act
            IActionResult result = await controller.EditPublisher(TestPublisherId, new PublisherInputModel { Name = "TestPub", Description = "Desc" });

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeletePublisher_Get_PublisherDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeletePublisher_Get_ServiceReturnsNull_RedirectsToIndex()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.GetPublisherForDeleteAsync(TestPublisherId))
                                          .ReturnsAsync((DeletePublisherViewModel?)null);

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeletePublisher_Get_Success_ReturnsView()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.GetPublisherForDeleteAsync(TestPublisherId))
                                          .ReturnsAsync(new DeletePublisherViewModel { Name = "TestPub" });

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeletePublisher_Post_PublisherDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId, new DeletePublisherViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeletePublisher_Post_ServiceFails_ReturnsView()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.HardDeletePublisherAsync(TestPublisherId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId, new DeletePublisherViewModel());

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task DeletePublisher_Post_Success_RedirectsToIndex()
        {
            // Arrange
            publisherServiceMock.Setup(ps => ps.PublisherExistsAsync(TestPublisherId)).ReturnsAsync(true);
            publisherManagementServiceMock.Setup(pms => pms.HardDeletePublisherAsync(TestPublisherId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.DeletePublisher(TestPublisherId, new DeletePublisherViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }
    }
}
