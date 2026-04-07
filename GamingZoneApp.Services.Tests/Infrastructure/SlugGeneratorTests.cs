using GamingZoneApp.Infrastructure.Utilities;
using GamingZoneApp.Infrastructure.Utilities.Interfaces;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Infrastructure
{
    [TestFixture]
    public class SlugGeneratorTests
    {
        private ISlugGenerator slugGenerator;

        [SetUp]
        public void SetUp()
        {
            slugGenerator = new SlugGenerator();
        }

        [Test]
        public void GenerateSlug_MultipleWords_ReturnsHyphenatedLowercase()
        {
            // Act
            string result = slugGenerator.GenerateSlug("The Legend Of Zelda");

            // Assert
            Assert.That(result, Is.EqualTo("the-legend-of-zelda"));
        }

        [Test]
        public void GenerateSlug_SingleWord_ReturnsLowercaseWord()
        {
            // Act
            string result = slugGenerator.GenerateSlug("Bloodborne");

            // Assert
            Assert.That(result, Is.EqualTo("bloodborne"));
        }

        [Test]
        public void GenerateSlug_AllUppercase_ReturnsAllLowercase()
        {
            // Act
            string result = slugGenerator.GenerateSlug("GRAND THEFT AUTO");

            // Assert
            Assert.That(result, Is.EqualTo("grand-theft-auto"));
        }

        [Test]
        public void GenerateSlug_ExtraSpaces_TrimsAndJoinsCorrectly()
        {
            // Act
            string result = slugGenerator.GenerateSlug("  Dark   Souls   III  ");

            // Assert
            Assert.That(result, Is.EqualTo("dark-souls-iii"));
        }

        [Test]
        public void GenerateSlug_MixedCase_ReturnsLowercaseSlug()
        {
            // Act
            string result = slugGenerator.GenerateSlug("Elden Ring");

            // Assert
            Assert.That(result, Is.EqualTo("elden-ring"));
        }

        [Test]
        public void GenerateSlug_EmptyString_ReturnsEmptyString()
        {
            // Act
            string result = slugGenerator.GenerateSlug("");

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GenerateSlug_WhitespaceOnly_ReturnsEmptyString()
        {
            // Act
            string result = slugGenerator.GenerateSlug("     ");

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
