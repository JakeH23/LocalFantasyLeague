using LocalFantasyLeague.Services.Components;
using LocalFantasyLeagueTests.Services.Util;

namespace LocalFantasyLeagueTests.Services.Components
{
    [TestFixture]
    public class TeamServiceTests : BaseServiceTest<TeamService>
    {
        private TeamService _teamService;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _teamService = new TeamService(ComponentService);
        }

        [Test]
        public async Task GetTeamIncludingPlayersAndTheirStats_ReturnsTeamWithPlayersAndStats()
        {
            // Arrange
            var teamId = 1; // Assuming team with ID 1 exists in TestDataSeeder

            // Act
            var result = await _teamService.GetTeamIncludingPlayersAndTheirStats(teamId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result!.Id, Is.EqualTo(teamId));
                Assert.That(result.Players, Is.Not.Null.And.Not.Empty);
                Assert.That(result.Players.All(p => p.PerformanceStats != null));
            });
        }

        [Test]
        public async Task GetTeamIncludingPlayersAndTheirStats_ReturnsNullForNonExistentTeam()
        {
            // Arrange
            var nonExistentTeamId = 999; // Assuming this team ID does not exist

            // Act
            var result = await _teamService.GetTeamIncludingPlayersAndTheirStats(nonExistentTeamId);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}