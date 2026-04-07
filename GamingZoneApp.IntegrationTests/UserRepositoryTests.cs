using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Models.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.IntegrationTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<GamingZoneDbContext> dbContextMock;
        private IUserRepository userRepository;

        private static readonly Guid TestUserId = Guid.NewGuid();
        private static readonly Guid AdminRoleId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<GamingZoneDbContext> options =
                new DbContextOptionsBuilder<GamingZoneDbContext>().Options;

            dbContextMock = new Mock<GamingZoneDbContext>(options);
            userRepository = new UserRepository(dbContextMock.Object);
        }

        [Test]
        public async Task GetAllUsersWithTheirRolesAsync_ReturnsUsersWithMappedRoles()
        {
            // Arrange
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = TestUserId, UserName = "admin", Email = "a@test.com" }
            };

            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = AdminRoleId, Name = "Admin" }
            };

            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid> { UserId = TestUserId, RoleId = AdminRoleId }
            };

            dbContextMock.Setup(c => c.Users).Returns(users.BuildMockDbSet().Object);
            dbContextMock.Setup(c => c.Roles).Returns(roles.BuildMockDbSet().Object);
            dbContextMock.Setup(c => c.UserRoles).Returns(userRoles.BuildMockDbSet().Object);

            // Act
            List<UserAllDto> result = (await userRepository.GetAllUsersWithTheirRolesAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("admin"));
            Assert.That(result[0].Roles, Does.Contain("Admin"));
        }

        [Test]
        public async Task GetAllRolesByNameAsync_ReturnsRoleNames()
        {
            // Arrange
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin" },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User" }
            };

            dbContextMock.Setup(c => c.Roles).Returns(roles.BuildMockDbSet().Object);

            // Act
            List<string?> result = (await userRepository.GetAllRolesByNameAsync()).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Does.Contain("Admin"));
            Assert.That(result, Does.Contain("User"));
        }

        [Test]
        public async Task AssignRoleToUserAsync_UserRole_ReturnsFalse()
        {
            // Act & Assert — "User" role is protected
            Assert.That(await userRepository.AssignRoleToUserAsync(TestUserId, "User"), Is.False);
        }

        [Test]
        public async Task AssignRoleToUserAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            dbContextMock.Setup(c => c.Users).Returns(new List<ApplicationUser>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.AssignRoleToUserAsync(TestUserId, "Admin"), Is.False);
        }

        [Test]
        public async Task AssignRoleToUserAsync_RoleNotFound_ReturnsFalse()
        {
            // Arrange
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = TestUserId }
            };

            dbContextMock.Setup(c => c.Users).Returns(users.BuildMockDbSet().Object);
            dbContextMock.Setup(c => c.Roles).Returns(new List<IdentityRole<Guid>>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.AssignRoleToUserAsync(TestUserId, "Admin"), Is.False);
        }

        [Test]
        public async Task AssignRoleToUserAsync_AlreadyInRole_ReturnsFalse()
        {
            // Arrange
            SetupUserRoleContext();

            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid> { UserId = TestUserId, RoleId = AdminRoleId }
            };

            dbContextMock.Setup(c => c.UserRoles).Returns(userRoles.BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.AssignRoleToUserAsync(TestUserId, "Admin"), Is.False);
        }

        [Test]
        public async Task AssignRoleToUserAsync_Success_AddsAndReturnsTrue()
        {
            // Arrange
            SetupUserRoleContext();

            Mock<DbSet<IdentityUserRole<Guid>>> mockUserRolesDbSet =
                new List<IdentityUserRole<Guid>>().BuildMockDbSet();

            dbContextMock.Setup(c => c.UserRoles).Returns(mockUserRolesDbSet.Object);

            // Act
            bool result = await userRepository.AssignRoleToUserAsync(TestUserId, "Admin");

            // Assert
            Assert.That(result, Is.True);

            mockUserRolesDbSet.Verify(d => d.Add(It.Is<IdentityUserRole<Guid>>(
                ur => ur.UserId == TestUserId && ur.RoleId == AdminRoleId)), Times.Once);

            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_UserRole_ReturnsFalse()
        {
            // Act & Assert — "User" role is protected
            Assert.That(await userRepository.RemoveRoleFromUserAsync(TestUserId, "User"), Is.False);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_RoleNotFound_ReturnsFalse()
        {
            // Arrange
            dbContextMock.Setup(c => c.Roles).Returns(new List<IdentityRole<Guid>>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.RemoveRoleFromUserAsync(TestUserId, "Admin"), Is.False);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_NotInRole_ReturnsFalse()
        {
            // Arrange
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = AdminRoleId, Name = "Admin" }
            };

            dbContextMock.Setup(c => c.Roles).Returns(roles.BuildMockDbSet().Object);

            dbContextMock.Setup(c => c.UserRoles)
                         .Returns(new List<IdentityUserRole<Guid>>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.RemoveRoleFromUserAsync(TestUserId, "Admin"), Is.False);
        }

        [Test]
        public async Task RemoveRoleFromUserAsync_Success_RemovesAndReturnsTrue()
        {
            // Arrange
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = AdminRoleId, Name = "Admin" }
            };

            IdentityUserRole<Guid> existingUserRole = new IdentityUserRole<Guid>
            {
                UserId = TestUserId,
                RoleId = AdminRoleId
            };

            Mock<DbSet<IdentityUserRole<Guid>>> mockUserRolesDbSet =
                new List<IdentityUserRole<Guid>> { existingUserRole }.BuildMockDbSet();

            dbContextMock.Setup(c => c.Roles).Returns(roles.BuildMockDbSet().Object);
            dbContextMock.Setup(c => c.UserRoles).Returns(mockUserRolesDbSet.Object);

            // Act
            bool result = await userRepository.RemoveRoleFromUserAsync(TestUserId, "Admin");

            // Assert
            Assert.That(result, Is.True);

            mockUserRolesDbSet.Verify(d => d.Remove(existingUserRole), Times.Once);

            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetDeleteUserAsync_Exists_ReturnsDto()
        {
            // Arrange
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = TestUserId, UserName = "admin", Email = "a@test.com" }
            };

            dbContextMock.Setup(c => c.Users).Returns(users.BuildMockDbSet().Object);

            // Act
            DeleteUserDto result = await userRepository.GetDeleteUserAsync(TestUserId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo("admin"));
            Assert.That(result.Email, Is.EqualTo("a@test.com"));
        }

        [Test]
        public async Task GetDeleteUserAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            dbContextMock.Setup(c => c.Users)
                         .Returns(new List<ApplicationUser>().BuildMockDbSet().Object);

            // Act
            DeleteUserDto result = await userRepository.GetDeleteUserAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task PostDeleteUserAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            dbContextMock.Setup(c => c.Users)
                         .Returns(new List<ApplicationUser>().BuildMockDbSet().Object);

            // Act & Assert
            Assert.That(await userRepository.PostDeleteUserAsync(Guid.NewGuid()), Is.False);
        }

        [Test]
        public async Task PostDeleteUserAsync_Success_RemovesGamesAndUserAndReturnsTrue()
        {
            // Arrange
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = TestUserId, UserName = "admin" }
            };

            List<Game> games = new List<Game>
            {
                new Game { Id = Guid.NewGuid(), UserId = TestUserId },
                new Game { Id = Guid.NewGuid(), UserId = Guid.NewGuid() }
            };

            Mock<DbSet<ApplicationUser>> mockUsersDbSet = users.BuildMockDbSet();
            Mock<DbSet<Game>> mockGamesDbSet = games.BuildMockDbSet();

            dbContextMock.Setup(c => c.Users).Returns(mockUsersDbSet.Object);
            dbContextMock.Setup(c => c.Games).Returns(mockGamesDbSet.Object);

            // Act
            bool result = await userRepository.PostDeleteUserAsync(TestUserId);

            // Assert
            Assert.That(result, Is.True);

            mockGamesDbSet.Verify(d => d.RemoveRange(It.IsAny<IEnumerable<Game>>()), Times.Once);

            mockUsersDbSet.Verify(d => d.Remove(It.Is<ApplicationUser>(u => u.Id == TestUserId)), Times.Once);

            dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private void SetupUserRoleContext()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = TestUserId }
            };

            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Id = AdminRoleId, Name = "Admin" }
            };

            dbContextMock.Setup(c => c.Users).Returns(users.BuildMockDbSet().Object);
            dbContextMock.Setup(c => c.Roles).Returns(roles.BuildMockDbSet().Object);
        }
    }
}