using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Seeding;
using GamingZoneApp.Data.Seeding.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Infrastructure
{
    [TestFixture]
    public class IdentitySeederTests
    {
        private Mock<RoleManager<IdentityRole<Guid>>> roleManagerMock;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<IConfiguration> configurationMock;
        private IIdentitySeeder identitySeeder;

        [SetUp]
        public void SetUp()
        {
            // RoleManager requires IRoleStore
            Mock<IRoleStore<IdentityRole<Guid>>> roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(
                roleStoreMock.Object, null!, null!, null!, null!);

            // UserManager requires IUserStore
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            configurationMock = new Mock<IConfiguration>();

            identitySeeder = new IdentitySeeder(
                roleManagerMock.Object,
                userManagerMock.Object,
                configurationMock.Object);
        }

        // ─────────────────────────────────────────────
        //  SeedRolesAsync
        // ─────────────────────────────────────────────

        [Test]
        public async Task SeedRolesAsync_NoRolesExist_CreatesAllThreeRoles()
        {
            // Arrange
            roleManagerMock.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>()))
                           .ReturnsAsync(false);

            roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await identitySeeder.SeedRolesAsync();

            // Assert
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Admin")), Times.Once);
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Moderator")), Times.Once);
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "User")), Times.Once);
        }

        [Test]
        public async Task SeedRolesAsync_AllRolesExist_SkipsCreation()
        {
            // Arrange
            roleManagerMock.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>()))
                           .ReturnsAsync(true);

            // Act
            await identitySeeder.SeedRolesAsync();

            // Assert
            roleManagerMock.Verify(rm => rm.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Never);
        }

        [Test]
        public async Task SeedRolesAsync_SomeRolesExist_CreatesOnlyMissingRoles()
        {
            // Arrange — "Admin" exists, others do not
            roleManagerMock.Setup(rm => rm.RoleExistsAsync("Admin"))
                           .ReturnsAsync(true);
            roleManagerMock.Setup(rm => rm.RoleExistsAsync("Moderator"))
                           .ReturnsAsync(false);
            roleManagerMock.Setup(rm => rm.RoleExistsAsync("User"))
                           .ReturnsAsync(false);

            roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await identitySeeder.SeedRolesAsync();

            // Assert
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Admin")), Times.Never);
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Moderator")), Times.Once);
            roleManagerMock.Verify(rm => rm.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "User")), Times.Once);
        }

        [Test]
        public void SeedRolesAsync_RoleCreationFails_ThrowsInvalidOperationException()
        {
            // Arrange
            roleManagerMock.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>()))
                           .ReturnsAsync(false);

            roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedRolesAsync());
        }

        // ─────────────────────────────────────────────
        //  SeedAdminUserAsync
        // ─────────────────────────────────────────────

        [Test]
        public void SeedAdminUserAsync_MissingUsername_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:AdminUser:Username"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedAdminUserAsync());
        }

        [Test]
        public void SeedAdminUserAsync_MissingEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:AdminUser:Username"]).Returns("admin");
            configurationMock.Setup(c => c["UserSeed:AdminUser:Email"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedAdminUserAsync());
        }

        [Test]
        public void SeedAdminUserAsync_MissingPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:AdminUser:Username"]).Returns("admin");
            configurationMock.Setup(c => c["UserSeed:AdminUser:Email"]).Returns("admin@test.com");
            configurationMock.Setup(c => c["UserSeed:AdminUser:Password"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedAdminUserAsync());
        }

        [Test]
        public async Task SeedAdminUserAsync_UserDoesNotExist_CreatesUserAndAssignsRole()
        {
            // Arrange
            SetupAdminConfig();

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync((ApplicationUser?)null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "Admin123!"))
                           .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(um => um.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                           .ReturnsAsync(false);

            userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await identitySeeder.SeedAdminUserAsync();

            // Assert
            userManagerMock.Verify(um => um.CreateAsync(It.Is<ApplicationUser>(u =>
                u.UserName == "admin" && u.Email == "admin@test.com"), "Admin123!"), Times.Once);
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Admin"), Times.Once);
        }

        [Test]
        public async Task SeedAdminUserAsync_UserExists_SkipsCreation()
        {
            // Arrange
            SetupAdminConfig();

            ApplicationUser existingAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync(existingAdmin);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(true);

            // Act
            await identitySeeder.SeedAdminUserAsync();

            // Assert
            userManagerMock.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task SeedAdminUserAsync_UserExistsNotInRole_AssignsRole()
        {
            // Arrange
            SetupAdminConfig();

            ApplicationUser existingAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync(existingAdmin);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(false);

            userManagerMock.Setup(um => um.AddToRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await identitySeeder.SeedAdminUserAsync();

            // Assert
            userManagerMock.Verify(um => um.AddToRoleAsync(existingAdmin, "Admin"), Times.Once);
        }

        [Test]
        public async Task SeedAdminUserAsync_UserExistsAlreadyInRole_DoesNothing()
        {
            // Arrange
            SetupAdminConfig();

            ApplicationUser existingAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync(existingAdmin);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(true);

            // Act
            await identitySeeder.SeedAdminUserAsync();

            // Assert
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void SeedAdminUserAsync_UserCreationFails_ThrowsInvalidOperationException()
        {
            // Arrange
            SetupAdminConfig();

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync((ApplicationUser?)null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "Admin123!"))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedAdminUserAsync());
        }

        [Test]
        public void SeedAdminUserAsync_AddToRoleFails_ThrowsInvalidOperationException()
        {
            // Arrange
            SetupAdminConfig();

            ApplicationUser existingAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("admin@test.com"))
                           .ReturnsAsync(existingAdmin);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(false);

            userManagerMock.Setup(um => um.AddToRoleAsync(existingAdmin, "Admin"))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedAdminUserAsync());
        }

        // ─────────────────────────────────────────────
        //  SeedModeratorUserAsync
        // ─────────────────────────────────────────────

        [Test]
        public void SeedModeratorUserAsync_MissingUsername_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Username"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedModeratorUserAsync());
        }

        [Test]
        public void SeedModeratorUserAsync_MissingEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Username"]).Returns("moderator");
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Email"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedModeratorUserAsync());
        }

        [Test]
        public void SeedModeratorUserAsync_MissingPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Username"]).Returns("moderator");
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Email"]).Returns("mod@test.com");
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Password"]).Returns((string?)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedModeratorUserAsync());
        }

        [Test]
        public async Task SeedModeratorUserAsync_UserDoesNotExist_CreatesUserAndAssignsRole()
        {
            // Arrange
            SetupModeratorConfig();

            userManagerMock.Setup(um => um.FindByEmailAsync("mod@test.com"))
                           .ReturnsAsync((ApplicationUser?)null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "Mod123!"))
                           .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(um => um.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Moderator"))
                           .ReturnsAsync(false);

            userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Moderator"))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await identitySeeder.SeedModeratorUserAsync();

            // Assert
            userManagerMock.Verify(um => um.CreateAsync(It.Is<ApplicationUser>(u =>
                u.UserName == "moderator" && u.Email == "mod@test.com"), "Mod123!"), Times.Once);
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Moderator"), Times.Once);
        }

        [Test]
        public async Task SeedModeratorUserAsync_UserExistsAlreadyInRole_DoesNothing()
        {
            // Arrange
            SetupModeratorConfig();

            ApplicationUser existingMod = new ApplicationUser
            {
                UserName = "moderator",
                Email = "mod@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("mod@test.com"))
                           .ReturnsAsync(existingMod);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingMod, "Moderator"))
                           .ReturnsAsync(true);

            // Act
            await identitySeeder.SeedModeratorUserAsync();

            // Assert
            userManagerMock.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
            userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void SeedModeratorUserAsync_UserCreationFails_ThrowsInvalidOperationException()
        {
            // Arrange
            SetupModeratorConfig();

            userManagerMock.Setup(um => um.FindByEmailAsync("mod@test.com"))
                           .ReturnsAsync((ApplicationUser?)null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), "Mod123!"))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedModeratorUserAsync());
        }

        [Test]
        public void SeedModeratorUserAsync_AddToRoleFails_ThrowsInvalidOperationException()
        {
            // Arrange
            SetupModeratorConfig();

            ApplicationUser existingMod = new ApplicationUser
            {
                UserName = "moderator",
                Email = "mod@test.com"
            };

            userManagerMock.Setup(um => um.FindByEmailAsync("mod@test.com"))
                           .ReturnsAsync(existingMod);

            userManagerMock.Setup(um => um.IsInRoleAsync(existingMod, "Moderator"))
                           .ReturnsAsync(false);

            userManagerMock.Setup(um => um.AddToRoleAsync(existingMod, "Moderator"))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await identitySeeder.SeedModeratorUserAsync());
        }

        // ─────────────────────────────────────────────
        //  Helper methods
        // ─────────────────────────────────────────────

        private void SetupAdminConfig()
        {
            configurationMock.Setup(c => c["UserSeed:AdminUser:Username"]).Returns("admin");
            configurationMock.Setup(c => c["UserSeed:AdminUser:Email"]).Returns("admin@test.com");
            configurationMock.Setup(c => c["UserSeed:AdminUser:Password"]).Returns("Admin123!");
        }

        private void SetupModeratorConfig()
        {
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Username"]).Returns("moderator");
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Email"]).Returns("mod@test.com");
            configurationMock.Setup(c => c["UserSeed:ModeratorUser:Password"]).Returns("Mod123!");
        }
    }
}
