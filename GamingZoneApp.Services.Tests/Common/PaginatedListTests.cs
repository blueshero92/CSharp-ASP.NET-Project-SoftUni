using GamingZoneApp.GCommon.Pagination;
using NUnit.Framework;

namespace GamingZoneApp.Services.Tests.Common
{
    [TestFixture]
    public class PaginatedListTests
    {
        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            List<string> items = new List<string> { "A", "B", "C" };

            // Act
            PaginatedList<string> paginatedList = new PaginatedList<string>(items, 10, 2, 3);

            // Assert
            Assert.That(paginatedList.PageIndex, Is.EqualTo(2));
            Assert.That(paginatedList.TotalPages, Is.EqualTo(4)); // ceil(10/3) = 4
            Assert.That(paginatedList, Has.Count.EqualTo(3));
        }

        [Test]
        public void HasPreviousPage_OnFirstPage_ReturnsFalse()
        {
            // Arrange
            PaginatedList<string> paginatedList = new PaginatedList<string>(new List<string>(), 10, 1, 5);

            // Assert
            Assert.That(paginatedList.HasPreviousPage, Is.False);
        }

        [Test]
        public void HasPreviousPage_OnSecondPage_ReturnsTrue()
        {
            // Arrange
            PaginatedList<string> paginatedList = new PaginatedList<string>(new List<string>(), 10, 2, 5);

            // Assert
            Assert.That(paginatedList.HasPreviousPage, Is.True);
        }

        [Test]
        public void HasNextPage_OnLastPage_ReturnsFalse()
        {
            // Arrange – 10 items, page size 5 → 2 pages, currently on page 2
            PaginatedList<string> paginatedList = new PaginatedList<string>(new List<string>(), 10, 2, 5);

            // Assert
            Assert.That(paginatedList.HasNextPage, Is.False);
        }

        [Test]
        public void HasNextPage_NotOnLastPage_ReturnsTrue()
        {
            // Arrange – 10 items, page size 5 → 2 pages, currently on page 1
            PaginatedList<string> paginatedList = new PaginatedList<string>(new List<string>(), 10, 1, 5);

            // Assert
            Assert.That(paginatedList.HasNextPage, Is.True);
        }

        [Test]
        public async Task CreateAsync_ReturnsCorrectPage()
        {
            // Arrange
            List<int> source = Enumerable.Range(1, 20).ToList();

            // Act – page 2, page size 5 → items 6..10
            PaginatedList<int> result = await PaginatedList<int>.CreateAsync(source, 2, 5);

            // Assert
            Assert.That(result.PageIndex, Is.EqualTo(2));
            Assert.That(result.TotalPages, Is.EqualTo(4)); // ceil(20/5) = 4
            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result[0], Is.EqualTo(6));
            Assert.That(result[4], Is.EqualTo(10));
        }

        [Test]
        public async Task CreateAsync_LastPagePartial_ReturnsRemainingItems()
        {
            // Arrange – 7 items, page 2, page size 5 → items 6,7
            List<int> source = Enumerable.Range(1, 7).ToList();

            // Act
            PaginatedList<int> result = await PaginatedList<int>.CreateAsync(source, 2, 5);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.TotalPages, Is.EqualTo(2));
            Assert.That(result.HasPreviousPage, Is.True);
            Assert.That(result.HasNextPage, Is.False);
        }

        [Test]
        public async Task CreateAsync_EmptySource_ReturnsEmptyList()
        {
            // Arrange
            List<int> source = new List<int>();

            // Act
            PaginatedList<int> result = await PaginatedList<int>.CreateAsync(source, 1, 5);

            // Assert
            Assert.That(result, Is.Empty);
            Assert.That(result.TotalPages, Is.EqualTo(0));
            Assert.That(result.HasPreviousPage, Is.False);
            Assert.That(result.HasNextPage, Is.False);
        }

        [Test]
        public async Task CreateAsync_SinglePage_NoPreviousOrNext()
        {
            // Arrange – 3 items, page size 5 → 1 page
            List<int> source = new List<int> { 1, 2, 3 };

            // Act
            PaginatedList<int> result = await PaginatedList<int>.CreateAsync(source, 1, 5);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result.TotalPages, Is.EqualTo(1));
            Assert.That(result.HasPreviousPage, Is.False);
            Assert.That(result.HasNextPage, Is.False);
        }
    }
}
