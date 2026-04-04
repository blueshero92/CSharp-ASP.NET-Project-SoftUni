using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.User;
using GamingZoneApp.ViewModels.Admin.User;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userService = new UserService(userRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_WithUsers_ReturnsMappedUsers()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<UserAllDto> usersDto = new List<UserAllDto>
            {
                new UserAllDto
                {
                    Id = userId,
                    Username = "TestUser",
                    Email = "testuser@test.com",
                    Roles = new List<string> { "User", "Admin" }
                }
            };

            userRepositoryMock.Setup(ur => ur.GetAllUsersWithTheirRolesAsync())
                              .ReturnsAsync(usersDto);

            // Act
            List<UserViewModel> result = (await userService.GetAllUsersAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(userId));
            Assert.That(result[0].Username, Is.EqualTo("TestUser"));
            Assert.That(result[0].Email, Is.EqualTo("testuser@test.com"));
            Assert.That(result[0].Roles, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetAllUsersAsync_NoUsers_ReturnsEmpty()
        {
            // Arrange
            userRepositoryMock.Setup(ur => ur.GetAllUsersWithTheirRolesAsync())
                              .ReturnsAsync(new List<UserAllDto>());

            // Act
            IEnumerable<UserViewModel> result = await userService.GetAllUsersAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllRolesAsync_WithRoles_ReturnsRoleNames()
        {
            // Arrange
            List<string?> roles = new List<string?> { "User", "Admin", "Moderator" };

            userRepositoryMock.Setup(ur => ur.GetAllRolesByNameAsync())
                              .ReturnsAsync(roles);

            // Act
            List<string> result = (await userService.GetAllRolesAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Does.Contain("Admin"));
            Assert.That(result, Does.Contain("User"));
            Assert.That(result, Does.Contain("Moderator"));
        }

        [Test]
        public async Task AssignRoleAsync_Success_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.AssignRoleToUserAsync(userId, "Admin"))
                              .ReturnsAsync(true);

            // Act
            bool result = await userService.AssignRoleAsync(userId, "Admin");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AssignRoleAsync_Failure_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.AssignRoleToUserAsync(userId, "Admin"))
                              .ReturnsAsync(false);

            // Act
            bool result = await userService.AssignRoleAsync(userId, "Admin");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveRoleAsync_Success_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.RemoveRoleFromUserAsync(userId, "Admin"))
                              .ReturnsAsync(true);

            // Act
            bool result = await userService.RemoveRoleAsync(userId, "Admin");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RemoveRoleAsync_Failure_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.RemoveRoleFromUserAsync(userId, "Admin"))
                              .ReturnsAsync(false);

            // Act
            bool result = await userService.RemoveRoleAsync(userId, "Admin");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetUserForDeletionAsync_UserExists_ReturnsDeleteViewModel()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            DeleteUserDto userDto = new DeleteUserDto
            {
                Id = userId,
                Username = "TestUser",
                Email = "testuser@test.com"
            };

            userRepositoryMock.Setup(ur => ur.GetDeleteUserAsync(userId))
                              .ReturnsAsync(userDto);

            // Act
            DeleteUserViewModel? result = await userService.GetUserForDeletionAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(userId));
            Assert.That(result.Username, Is.EqualTo("TestUser"));
            Assert.That(result.Email, Is.EqualTo("testuser@test.com"));
        }

        [Test]
        public async Task GetUserForDeletionAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock?.Setup(ur => ur.GetDeleteUserAsync(userId))
                              .ReturnsAsync((DeleteUserDto?)null);

            // Act
            DeleteUserViewModel? result = await userService.GetUserForDeletionAsync(userId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteUserAsync_Success_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.PostDeleteUserAsync(userId))
                              .ReturnsAsync(true);

            // Act
            bool result = await userService.DeleteUserAsync(userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteUserAsync_Failure_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            userRepositoryMock.Setup(ur => ur.PostDeleteUserAsync(userId))
                              .ReturnsAsync(false);

            // Act
            bool result = await userService.DeleteUserAsync(userId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}