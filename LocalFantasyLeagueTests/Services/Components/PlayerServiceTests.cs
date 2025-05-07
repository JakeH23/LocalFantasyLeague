using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class PlayerServiceTests : BaseServiceTest<PlayerService>
    {
        private PlayerService _playerService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _playerService = new PlayerService(ComponentService);
        }

        [Test]
        public async Task GetPlayerListIncludingTeam_ReturnsPlayersWithTeams()
        {
            // Act
            var result = await _playerService.GetPlayerListIncludingTeam();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(13)); // Assuming 13 players in TestDataSeeder
            Assert.That(result.All(p => p.Team != null));
        }

        [Test]
        public async Task GetPlayerListIncludingStatsAndTeam_ReturnsPlayersWithStatsAndTeams()
        {
            // Act
            var result = await _playerService.GetPlayerListIncludingStatsAndTeam();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(13)); // Assuming 13 players in TestDataSeeder
            Assert.That(result.All(p => p.Team != null && p.PerformanceStats != null));
        }

        [Test]
        public async Task GetPlayerListFilteredByPlayerIdList_ReturnsFilteredPlayers()
        {
            // Arrange
            var playerIds = new List<int> { 1, 2, 3 };

            // Act
            var result = await _playerService.GetPlayerListFilteredByPlayerIdList(playerIds);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result.All(p => playerIds.Contains(p.Id)));
        }

        [Test]
        public async Task GetPlayerListIncludingTeamFilteredByMatchId_ReturnsPlayersForMatchTeams()
        {
            // Arrange
            var matchId = 1;

            // Act
            var result = await _playerService.GetPlayerListIncludingTeamFilteredByMatchId(matchId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0)); // Assuming players exist for the match teams
        }

        [Test]
        public async Task GetPlayerListIncludingTeamFilteredByTeamIds_ReturnsPlayersForTeams()
        {
            // Arrange
            var homeTeamId = 1;
            var awayTeamId = 2;

            // Act
            var result = await _playerService.GetPlayerListIncludingTeamFilteredByTeamIds(homeTeamId, awayTeamId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(0)); // Assuming players exist for the teams
            Assert.That(result.All(p => p.TeamId == homeTeamId || p.TeamId == awayTeamId));
        }

        [Test]
        public async Task GetPlayerIncludingTeamAndStatsFilteringByPlayerId_ReturnsPlayerWithDetails()
        {
            // Arrange
            var playerId = 1;

            // Act
            var result = await _playerService.GetPlayerIncludingTeamAndStatsFilteringByPlayerId(playerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result!.Id, Is.EqualTo(playerId));
                Assert.That(result.Team, Is.Not.Null);
                Assert.That(result.PerformanceStats, Is.Not.Empty);
            });
        }

        [Test]
        public async Task GetPlayerIncludingTeamAndStatDetailsAcrossMatchAndSeason_ReturnsPlayerWithFullDetails()
        {
            // Arrange
            var playerId = 1;

            // Act
            var result = await _playerService.GetPlayerIncludingTeamAndStatDetailsAcrossMatchAndSeason(playerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result!.Id, Is.EqualTo(playerId));
                Assert.That(result.Team, Is.Not.Null);
                Assert.That(result.PerformanceStats, Is.Not.Empty);
                Assert.That(result.PerformanceStats.All(ps => ps.Match != null && ps.Match.Season != null));
                Assert.That(result.PerformanceStats.All(ps => ps.Match.HomeTeam != null && ps.Match.AwayTeam != null));
            });
        }
    }
}
