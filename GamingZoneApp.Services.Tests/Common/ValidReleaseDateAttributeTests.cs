using System.ComponentModel.DataAnnotations;
using GamingZoneApp.GCommon.CustomValidationAttributes;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Common
{
    [TestFixture]
    public class ValidReleaseDateAttributeTests
    {
        private ValidReleaseDateAttribute attribute;
        private ValidationContext validationContext;

        [SetUp]
        public void SetUp()
        {
            attribute = new ValidReleaseDateAttribute();
            validationContext = new ValidationContext(new object());
        }

        [Test]
        public void IsValid_DateTimeToday_ReturnsSuccess()
        {
            // Act
            ValidationResult? result = attribute.GetValidationResult(DateTime.Today, validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_DateTimePastValid_ReturnsSuccess()
        {
            // Arrange
            DateTime pastDate = new DateTime(2020, 6, 15);

            // Act
            ValidationResult? result = attribute.GetValidationResult(pastDate, validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_DateTimeFuture_ReturnsError()
        {
            // Arrange
            DateTime futureDate = DateTime.Today.AddDays(1);

            // Act
            ValidationResult? result = attribute.GetValidationResult(futureDate, validationContext);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("Release date can't be in the future."));
        }

        [Test]
        public void IsValid_ValidDateString_ReturnsSuccess()
        {
            // Arrange — format is "yyyy-MM-dd"
            string dateString = "2020-06-15";

            // Act
            ValidationResult? result = attribute.GetValidationResult(dateString, validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_InvalidDateStringFormat_ReturnsFormatError()
        {
            // Arrange — wrong format
            string dateString = "15/06/2020";

            // Act
            ValidationResult? result = attribute.GetValidationResult(dateString, validationContext);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("format"));
        }

        [Test]
        public void IsValid_FutureDateString_ReturnsError()
        {
            // Arrange
            string futureDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");

            // Act
            ValidationResult? result = attribute.GetValidationResult(futureDate, validationContext);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("Release date can't be in the future."));
        }

        [Test]
        public void IsValid_YearBeforeMinimum_ReturnsError()
        {
            // Arrange — minimum year is 1958
            DateTime tooOld = new DateTime(1950, 1, 1);

            // Act
            ValidationResult? result = attribute.GetValidationResult(tooOld, validationContext);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("1958"));
        }

        [Test]
        public void IsValid_YearBeforeMinimumAsString_ReturnsError()
        {
            // Arrange
            string tooOldDate = "1950-01-01";

            // Act
            ValidationResult? result = attribute.GetValidationResult(tooOldDate, validationContext);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("1958"));
        }

        [Test]
        public void IsValid_ExactMinimumYear_ReturnsSuccess()
        {
            // Arrange — 1958 is the minimum valid year
            DateTime minDate = new DateTime(1958, 1, 1);

            // Act
            ValidationResult? result = attribute.GetValidationResult(minDate, validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_NullValue_ReturnsSuccess()
        {
            // Act
            ValidationResult? result = attribute.GetValidationResult(null, validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_EmptyString_ReturnsSuccess()
        {
            // Act
            ValidationResult? result = attribute.GetValidationResult("", validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        public void IsValid_WhitespaceString_ReturnsSuccess()
        {
            // Act
            ValidationResult? result = attribute.GetValidationResult("   ", validationContext);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }
    }
}