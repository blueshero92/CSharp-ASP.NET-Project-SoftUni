using GamingZoneApp.Areas.Admin.Controllers;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class UserManagementControllerTests
    {
        private Mock<IUserService> userServiceMock;
        private UserManagementController controller;

        private static readonly Guid AdminUserId = Guid.NewGuid();
        private static readonly Guid TestUserId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            userServiceMock = new Mock<IUserService>();

            controller = new UserManagementController(userServiceMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, AdminUserId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, "TestAuth"))
                }
            };

            controller.TempData = new TempDataDictionary(
                controller.HttpContext,
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public async Task Index_ReturnsViewWithUsersExcludingCurrentAdmin()
        {
            // Arrange
            List<UserViewModel> users = new List<UserViewModel>
            {
                new UserViewModel { Id = AdminUserId, Username = "Admin" },
                new UserViewModel { Id = TestUserId, Username = "TestUser" }
            };

            userServiceMock.Setup(us => us.GetAllUsersAsync()).ReturnsAsync(users);
            userServiceMock.Setup(us => us.GetAllRolesAsync()).ReturnsAsync(new List<string> { "User", "Admin" });

            // Act
            IActionResult result = await controller.Index();

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);

            IEnumerable<UserViewModel> model = viewResult!.Model as IEnumerable<UserViewModel>;
            Assert.That(model!.Count(), Is.EqualTo(1));
            Assert.That(model!.First().Username, Is.EqualTo("TestUser"));
        }

        [Test]
        public async Task AssignRole_EmptyRole_RedirectsToIndex()
        {
            // Arrange & Act
            IActionResult result = await controller.AssignRole(TestUserId, string.Empty);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AssignRole_UserRole_RedirectsWithError()
        {
            // Arrange & Act
            IActionResult result = await controller.AssignRole(TestUserId, "User");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AssignRole_ServiceFails_RedirectsWithError()
        {
            // Arrange
            userServiceMock.Setup(us => us.AssignRoleAsync(TestUserId, "Admin")).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.AssignRole(TestUserId, "Admin");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task AssignRole_Success_RedirectsToIndex()
        {
            // Arrange
            userServiceMock.Setup(us => us.AssignRoleAsync(TestUserId, "Admin")).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.AssignRole(TestUserId, "Admin");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task RemoveRole_EmptyRole_RedirectsToIndex()
        {
            // Arrange & Act
            IActionResult result = await controller.RemoveRole(TestUserId, string.Empty);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task RemoveRole_UserRole_RedirectsWithError()
        {
            // Arrange & Act
            IActionResult result = await controller.RemoveRole(TestUserId, "User");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task RemoveRole_ServiceFails_RedirectsWithError()
        {
            // Arrange
            userServiceMock.Setup(us => us.RemoveRoleAsync(TestUserId, "Admin")).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.RemoveRole(TestUserId, "Admin");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task RemoveRole_Success_RedirectsToIndex()
        {
            // Arrange
            userServiceMock.Setup(us => us.RemoveRoleAsync(TestUserId, "Admin")).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.RemoveRole(TestUserId, "Admin");

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteUser_Get_UserNotFound_RedirectsToIndex()
        {
            // Arrange
            userServiceMock.Setup(us => us.GetUserForDeletionAsync(TestUserId))
                           .ReturnsAsync((DeleteUserViewModel?)null);

            // Act
            IActionResult result = await controller.DeleteUser(TestUserId);

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteUser_Get_UserFound_ReturnsView()
        {
            // Arrange
            userServiceMock.Setup(us => us.GetUserForDeletionAsync(TestUserId))
                           .ReturnsAsync(new DeleteUserViewModel { Id = TestUserId, Username = "TestUser", Email = "test@test.com" });

            // Act
            IActionResult result = await controller.DeleteUser(TestUserId);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(((DeleteUserViewModel)viewResult!.Model!).Username, Is.EqualTo("TestUser"));
        }

        [Test]
        public async Task DeleteUser_Post_ServiceFails_RedirectsToIndex()
        {
            // Arrange
            userServiceMock.Setup(us => us.DeleteUserAsync(TestUserId)).ReturnsAsync(false);

            // Act
            IActionResult result = await controller.DeleteUser(TestUserId, new DeleteUserViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteUser_Post_Success_RedirectsToIndex()
        {
            // Arrange
            userServiceMock.Setup(us => us.DeleteUserAsync(TestUserId)).ReturnsAsync(true);

            // Act
            IActionResult result = await controller.DeleteUser(TestUserId, new DeleteUserViewModel());

            // Assert
            RedirectToActionResult redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
        }
    }
}
