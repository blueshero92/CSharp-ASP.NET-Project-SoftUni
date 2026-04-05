using GamingZoneApp.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;
using NUnit.Framework;

using System.Security.Claims;

namespace GamingZoneApp.Services.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> loggerMock;
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger<HomeController>>();
            controller = new HomeController(loggerMock.Object);

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
        public void Index_ReturnsViewResult()
        {
            // Act
            IActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Privacy_ReturnsViewResult()
        {
            // Act
            IActionResult result = controller.Privacy();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Error_400_ReturnsBadRequestView()
        {
            // Act
            IActionResult result = controller.Error(400);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult!.ViewName, Is.EqualTo("BadRequest"));
        }

        [Test]
        public void Error_403_ReturnsForbiddenView()
        {
            // Act
            IActionResult result = controller.Error(403);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult!.ViewName, Is.EqualTo("Forbidden"));
        }

        [Test]
        public void Error_404_ReturnsNotFoundView()
        {
            // Act
            IActionResult result = controller.Error(404);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult!.ViewName, Is.EqualTo("NotFound"));
        }

        [Test]
        public void Error_500_ReturnsServerErrorView()
        {
            // Act
            IActionResult result = controller.Error(500);

            // Assert
            ViewResult viewResult = result as ViewResult;
            Assert.That(viewResult!.ViewName, Is.EqualTo("ServerError"));
        }

        [Test]
        public void Error_UnknownStatusCode_ReturnsDefaultErrorView()
        {
            // Act
            IActionResult result = controller.Error(418);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
