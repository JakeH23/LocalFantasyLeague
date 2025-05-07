using LocalFantasyLeague.Models;
using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;
using Microsoft.EntityFrameworkCore;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class PerformanceStatsServiceTests : BaseServiceTest<PerformanceStatService>
    {
        private PerformanceStatService _performanceStatService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _performanceStatService = new PerformanceStatService(ComponentService);
        }

        [Test]
        public async Task GetPerformanceStatListFilteredByMatchAndSelectedPlayers_ReturnsFilteredStats()
        {
            // Arrange
            var matchId = 1;
            var selectedPlayers = Context.Players.Take(2).ToList();

            // Act
            var result = await _performanceStatService.GetPerformanceStatListFilteredByMatchAndSelectedPlayers(matchId, selectedPlayers);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.All(ps => ps.MatchId == matchId && selectedPlayers.Select(p => p.Id).Contains(ps.PlayerId)));
        }

        [Test]
        public async Task GetPerformanceStatListFilteredByMatchIds_ReturnsStatsForMatchIds()
        {
            // Arrange
            var matchIds = new List<int> { 1, 2 };

            // Act
            var result = await _performanceStatService.GetPerformanceStatListFilteredByMatchIds(matchIds);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(26));
            Assert.That(result.All(ps => matchIds.Contains(ps.MatchId)));
        }

        [Test]
        public async Task GetPerformanceStatFilteredByPlayerId_ReturnsLatestStatForPlayer()
        {
            // Arrange
            var playerId = 1;

            // Act
            var result = await _performanceStatService.GetPerformanceStatFilteredByPlayerId(playerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.Appearance, Is.EqualTo(true));
        }

        [Test]
        public async Task GetTopThreeFantasyPointScorers_ReturnsTopScorers()
        {
            // Act
            var result = await _performanceStatService.GetTopThreeFantasyPointScorers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result, Is.Ordered.By(nameof(FantasyScorer.FantasyPoints)).Descending);
        }

        [Test]
        public async Task GetTopThreeFantasyPointScorersByTeamId_ReturnsTopScorersForTeam()
        {
            // Arrange
            var teamId = 1;

            // Act
            var result = await _performanceStatService.GetTopThreeFantasyPointScorersByTeamId(teamId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Ordered.By(nameof(FantasyScorer.FantasyPoints)).Descending);
            });
        }
    }
}
