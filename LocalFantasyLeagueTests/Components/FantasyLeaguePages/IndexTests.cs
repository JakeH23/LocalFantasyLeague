using Bunit;
using LocalFantasyLeagueTests.Components.Util;

namespace LocalFantasyLeagueTests.Components.FantasyLeaguePages
{
    [TestFixture]
    public class IndexTests : ComponentSetup
    {
        [Test]
        public void OnInitializedAsync_LoadsSeasonsAndLeagueData()
        {
            // Arrange
            var component = RenderComponent<LocalFantasyLeague.Components.Pages.FantasyLeaguePages.Index>();

            // Act
            // No need to explicitly call OnInitializedAsync; it is invoked automatically.

            // Assert
            Assert.That(component.Instance.Seasons, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(component.Instance.Seasons, Has.Count.EqualTo(1));
                Assert.That(component.Instance.LeagueEntries, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadLeagueData_CalculatesTotalPointsCorrectly()
        {
            // Arrange
            var component = RenderComponent<LocalFantasyLeague.Components.Pages.FantasyLeaguePages.Index>();

            // Act
            await component.InvokeAsync(() => component.Instance.LoadLeagueData());

            // Assert
            var leagueEntries = component.Instance.LeagueEntries;
            Assert.That(leagueEntries, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(leagueEntries.First(le => le.Username == "User1").TotalPoints, Is.EqualTo(13)); // 12 (captain) + 1
                Assert.That(leagueEntries.First(le => le.Username == "User2").TotalPoints, Is.EqualTo(32)); // 22 (captain) + 10
            });
        }

        [Test]
        public async Task FilterBySeason_FiltersLeagueEntriesBySelectedSeason()
        {
            // Arrange
            var component = RenderComponent<LocalFantasyLeague.Components.Pages.FantasyLeaguePages.Index>();
            component.Instance.SelectedSeasonId = 1;

            // Act
            await component.InvokeAsync(() => component.Instance.FilterBySeason());

            // Assert
            var leagueEntries = component.Instance.LeagueEntries;
            Assert.That(leagueEntries, Is.Not.Null);
            Assert.That(leagueEntries.All(entry => new[] { "User1", "User2" }.Contains(entry.Username)));
        }
    }
}
