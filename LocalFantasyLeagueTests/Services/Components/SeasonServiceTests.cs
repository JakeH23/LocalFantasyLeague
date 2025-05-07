using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class SeasonServiceTests : BaseServiceTest<SeasonService>
    {
        private SeasonService _seasonService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _seasonService = new SeasonService(ComponentService);
        }

        [Test]
        public async Task GetSeasonListOrderedByDescendingStartDate_ReturnsSeasonsInDescendingOrder()
        {
            // Act
            var result = await _seasonService.GetSeasonListOrderedByDescendingStartDate();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Is.Ordered.By(nameof(Season.StartDate)).Descending);
        }
    }
}