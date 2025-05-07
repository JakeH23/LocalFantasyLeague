using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class MatchServiceTests : BaseServiceTest<MatchService>
    {
        private MatchService _matchService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _matchService = new MatchService(ComponentService);
        }

        [Test]
        public async Task GetMatchListIncludingTeamsAndSeason_ReturnsMatchesWithTeamsAndSeason()
        {
            // Act
            var result = await _matchService.GetMatchListIncludingTeamsAndSeason();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            Assert.That(result.All(m => m.HomeTeam != null && m.AwayTeam != null && m.Season != null));
        }

        [Test]
        public async Task GetMatchListIncludingTeamsAndStats_ReturnsMatchesWithTeamsAndStats()
        {
            // Act
            var result = await _matchService.GetMatchListIncludingTeamsAndStats();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            Assert.That(result.All(m => m.HomeTeam != null && m.AwayTeam != null && m.Stats != null));
        }

        [Test]
        public async Task GetMatchListIncludingTeamsForCurrentUserTeamId_ReturnsMatchesForTeam()
        {
            // Act
            var result = await _matchService.GetMatchListIncludingTeamsForCurrentUserTeamId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            Assert.That(result.All(m => m.HomeTeamId == 1 || m.AwayTeamId == 1));
        }

        [Test]
        public async Task GetMatchListIncludingTeamsFilteredBySeasonId_ReturnsMatchesForSeason()
        {
            // Act
            var result = await _matchService.GetMatchListIncludingTeamsFilteredBySeasonId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
            Assert.That(result.All(m => m.SeasonId == 1));
        }

        [Test]
        public async Task GetMatchListIncludingSeasonsFilteredByTeamId_ReturnsMatchesForTeamWithSeason()
        {
            // Act
            var result = await _matchService.GetMatchListIncludingSeasonsFilteredByTeamId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(10));
            Assert.That(result.All(m => m.HomeTeamId == 1 || m.AwayTeamId == 1));
        }

        [Test]
        public async Task GetMatchIdListFilteredBySeasonId_ReturnsMatchIdsForSeason()
        {
            // Act
            var result = await _matchService.GetMatchIdListFilteredBySeasonId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(9));
        }

        [Test]
        public async Task GetMatchIncludingTeamsByMatchId_ReturnsMatchWithTeams()
        {
            // Act
            var result = await _matchService.GetMatchIncludingTeamsByMatchId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.HomeTeam, Is.Not.Null);
            Assert.That(result.AwayTeam, Is.Not.Null);
        }

        [Test]
        public async Task GetMatchIncludingTeamsAndPlayersAndStatsByMatchId_ReturnsMatchWithDetails()
        {
            // Act
            var result = await _matchService.GetMatchIncludingTeamsAndPlayersAndStatsByMatchId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.HomeTeam, Is.Not.Null);
            Assert.That(result.AwayTeam, Is.Not.Null);
            Assert.That(result.Stats, Is.Not.Empty);
        }

        [Test]
        public async Task GetPreviousFiveMatches_ReturnsMatchesInThePast()
        {
            // Act
            var result = await _matchService.GetPreviousFiveMatches();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result.All(m => m.Kickoff < DateTime.Now));
        }

        [Test]
        public async Task GetNextFiveMatches_ReturnsMatchesInTheFuture()
        {
            // Act
            var result = await _matchService.GetNextFiveMatches();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.All(m => m.Kickoff > DateTime.Now));
        }

        [Test]
        public async Task GetPreviousFiveMatchesByTeamId_ReturnsPastMatchesForTeam()
        {
            // Act
            var result = await _matchService.GetPreviousFiveMatchesByTeamId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(5));
            Assert.That(result.All(m => m.Kickoff < DateTime.Now && (m.HomeTeamId == 1 || m.AwayTeamId == 1)));
        }

        [Test]
        public async Task GetNextFiveMatchesByTeamId_ReturnsFutureMatchesForTeam()
        {
            // Act
            var result = await _matchService.GetNextFiveMatchesByTeamId(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.All(m => m.Kickoff > DateTime.Now && (m.HomeTeamId == 1 || m.AwayTeamId == 1)));
        }

        [Test]
        public async Task GetMatchListRequiringStatsToBeEntered_ReturnsMatchesWithoutStats()
        {
            // Act
            var result = await _matchService.GetMatchListRequiringStatsToBeEntered();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(5));
        }

        [Test]
        public async Task GetMatchListForNextSevenDaysRequiringFantasyPicks_ReturnsMatchesWithoutSelections()
        {
            // Arrange
            var userSelections = new List<int> { 1 };

            // Act
            var result = await _matchService.GetMatchListForNextSevenDaysRequiringFantasyPicks(userSelections);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.All(m => m.Id == 10)); // Match 2 is in the next 7 days and not in userSelections
        }
    }
}
