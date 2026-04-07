using GamingZoneApp.Data.Seeding.Interfaces;
using GamingZoneApp.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Moq;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Infrastructure
{
    [TestFixture]
    public class WebApplicationExtensionsTests
    {
        private Mock<IApplicationBuilder> appBuilderMock;
        private Mock<IIdentitySeeder> identitySeederMock;
        private Mock<IServiceProvider> scopedProviderMock;
        private Mock<IServiceScope> serviceScopeMock;
        private Mock<IServiceScopeFactory> scopeFactoryMock;
        private Mock<IServiceProvider> rootProviderMock;

        [SetUp]
        public void SetUp()
        {
            identitySeederMock = new Mock<IIdentitySeeder>();
            scopedProviderMock = new Mock<IServiceProvider>();
            serviceScopeMock = new Mock<IServiceScope>();
            scopeFactoryMock = new Mock<IServiceScopeFactory>();
            rootProviderMock = new Mock<IServiceProvider>();
            appBuilderMock = new Mock<IApplicationBuilder>();

            // Wire up the DI chain:
            // IApplicationBuilder.ApplicationServices → root IServiceProvider
            // root IServiceProvider → IServiceScopeFactory → IServiceScope → scoped IServiceProvider → IIdentitySeeder
            scopedProviderMock.Setup(sp => sp.GetService(typeof(IIdentitySeeder)))
                              .Returns(identitySeederMock.Object);

            serviceScopeMock.Setup(s => s.ServiceProvider)
                            .Returns(scopedProviderMock.Object);

            scopeFactoryMock.Setup(f => f.CreateScope())
                            .Returns(serviceScopeMock.Object);

            rootProviderMock.Setup(sp => sp.GetService(typeof(IServiceScopeFactory)))
                            .Returns(scopeFactoryMock.Object);

            appBuilderMock.Setup(ab => ab.ApplicationServices)
                          .Returns(rootProviderMock.Object);
        }

        [Test]
        public void UseRolesSeeder_CallsSeedRolesAsync()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedRolesAsync())
                              .Returns(Task.CompletedTask);

            // Act
            IApplicationBuilder result = appBuilderMock.Object.UseRolesSeeder();

            // Assert
            identitySeederMock.Verify(s => s.SeedRolesAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(appBuilderMock.Object));
        }

        [Test]
        public void UseRolesSeeder_CreatesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedRolesAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseRolesSeeder();

            // Assert
            scopeFactoryMock.Verify(f => f.CreateScope(), Times.Once);
        }

        [Test]
        public void UseAdminUserSeeder_CallsSeedAdminUserAsync()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedAdminUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            IApplicationBuilder result = appBuilderMock.Object.UseAdminUserSeeder();

            // Assert
            identitySeederMock.Verify(s => s.SeedAdminUserAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(appBuilderMock.Object));
        }

        [Test]
        public void UseAdminUserSeeder_CreatesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedAdminUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseAdminUserSeeder();

            // Assert
            scopeFactoryMock.Verify(f => f.CreateScope(), Times.Once);
        }

        [Test]
        public void UseModeratorUserSeeder_CallsSeedModeratorUserAsync()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedModeratorUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            IApplicationBuilder result = appBuilderMock.Object.UseModeratorUserSeeder();

            // Assert
            identitySeederMock.Verify(s => s.SeedModeratorUserAsync(), Times.Once);
            Assert.That(result, Is.EqualTo(appBuilderMock.Object));
        }

        [Test]
        public void UseModeratorUserSeeder_CreatesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedModeratorUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseModeratorUserSeeder();

            // Assert
            scopeFactoryMock.Verify(f => f.CreateScope(), Times.Once);
        }

        [Test]
        public void UseRolesSeeder_DisposesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedRolesAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseRolesSeeder();

            // Assert
            serviceScopeMock.Verify(s => s.Dispose(), Times.Once);
        }

        [Test]
        public void UseAdminUserSeeder_DisposesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedAdminUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseAdminUserSeeder();

            // Assert
            serviceScopeMock.Verify(s => s.Dispose(), Times.Once);
        }

        [Test]
        public void UseModeratorUserSeeder_DisposesScope()
        {
            // Arrange
            identitySeederMock.Setup(s => s.SeedModeratorUserAsync())
                              .Returns(Task.CompletedTask);

            // Act
            appBuilderMock.Object.UseModeratorUserSeeder();

            // Assert
            serviceScopeMock.Verify(s => s.Dispose(), Times.Once);
        }
    }
}