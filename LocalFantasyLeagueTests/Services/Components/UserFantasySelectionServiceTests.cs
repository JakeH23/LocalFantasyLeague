using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class UserFantasySelectionServiceTests : BaseServiceTest<UserFantasySelectionService>
    {
        private UserFantasySelectionService _userFantasySelectionService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _userFantasySelectionService = new UserFantasySelectionService(ComponentService);
        }

        [Test]
        public async Task GetUserFantasySelectionListFilteredByUserId_ReturnsSelectionsForUser()
        {
            // Arrange
            var userId = 1; // Assuming user with ID 1 exists in TestDataSeeder

            // Act
            var result = await _userFantasySelectionService.GetUserFantasySelectionListFilteredByUserId(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0)); // Assuming user has selections
            Assert.That(result.All(s => s.UserId == userId));
        }

        [Test]
        public async Task GetUserFantasySelectionListFilteredByUserId_ReturnsEmptyListForNonExistentUser()
        {
            // Arrange
            var nonExistentUserId = 999; // Assuming this user ID does not exist

            // Act
            var result = await _userFantasySelectionService.GetUserFantasySelectionListFilteredByUserId(nonExistentUserId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetUserFantasySelectionListOfMatchIdsFilteredByUserId_ReturnsMatchIdsForUser()
        {
            // Arrange
            var userId = 1; // Assuming user with ID 1 exists in TestDataSeeder

            // Act
            var result = await _userFantasySelectionService.GetUserFantasySelectionListOfMatchIdsFilteredByUserId(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0)); // Assuming user has selections
        }

        [Test]
        public async Task GetUserFantasySelectionListOfMatchIdsFilteredByUserId_ReturnsEmptyListForNonExistentUser()
        {
            // Arrange
            var nonExistentUserId = 999; // Assuming this user ID does not exist

            // Act
            var result = await _userFantasySelectionService.GetUserFantasySelectionListOfMatchIdsFilteredByUserId(nonExistentUserId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
    }
}
