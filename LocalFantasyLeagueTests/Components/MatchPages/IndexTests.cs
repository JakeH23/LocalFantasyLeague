using Bunit;
using LocalFantasyLeagueTests.Components.Util;
using Moq;
using Match = LocalFantasyLeague.Models.DbSets.Match;

namespace LocalFantasyLeagueTests.Components.MatchPages
{
    [TestFixture]
    public class IndexTests : ComponentSetup
    {
        [Test]
        public void Index_ShouldRenderMatchesTable()
        {
            // Act
            var component = RenderComponent<LocalFantasyLeague.Components.Pages.MatchPages.Index>();

            // Assert
            Assert.That(component.Markup, Does.Contain("<th>Kickoff</th>"));
            Assert.That(component.Markup, Does.Contain("<th>Home</th>"));
            Assert.That(component.Markup, Does.Contain("<th>Away</th>"));
            Assert.That(component.Markup, Does.Contain("<th>Result</th>"));

            // Check for match data
            Assert.That(component.Markup, Does.Contain("Team A"));
            Assert.That(component.Markup, Does.Contain("Team B"));
        }

        [Test]
        public void Index_ShouldApplyFilters()
        {
            var component = RenderComponent<LocalFantasyLeague.Components.Pages.MatchPages.Index>();

            // Act
            component.Instance.SelectedSeasonId = 1;
            component.Instance.SelectedTeamId = 1;

            // Assert
            Assert.That(component.Instance.PaginatedMatches, Has.Count.EqualTo(1));
        }

        [Test]
        public void Index_ShouldHandlePagination()
        {
            // Arrange
            var matches = Enumerable.Range(1, 25).Select(i => new Match { Id = i }).ToList();

            _mockMatchService.Setup(s => s.GetMatchListIncludingTeamsAndSeason())
                .ReturnsAsync(matches);

            var component = RenderComponent<LocalFantasyLeague.Components.Pages.MatchPages.Index>();

            // Act
            component.Instance.NextPage();
            component.Instance.PreviousPage();

            // Assert
            Assert.That(component.Instance._currentPage, Is.EqualTo(1));
        }
    }
}
