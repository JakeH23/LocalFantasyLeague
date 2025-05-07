using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Match = LocalFantasyLeague.Models.DbSets.Match;

namespace LocalFantasyLeagueTests.Services
{
    [TestFixture]
    public class DeleteServiceTests
    {
        private DbContextOptions<LocalFantasyLeagueContext> _dbContextOptions;

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<LocalFantasyLeagueContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique DB for each test
                .Options;
        }

        private static async Task SeedTestData(LocalFantasyLeagueContext context)
        {
            context.Matches.Add(new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2 });
            context.PerformanceStats.Add(new PerformanceStat { Id = 1, MatchId = 1, PlayerId = 1 });
            context.UserFantasySelections.Add(new UserFantasySelection { Id = 1, MatchId = 1, UserId = 1 });
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task DeleteAllCorrespondingMatchData_RemovesRelatedData()
        {
            // Arrange
            const int MatchId = 1;

            await using (var context = new LocalFantasyLeagueContext(_dbContextOptions))
            {
                await SeedTestData(context);

                var dbContextFactory = new Mock<IDbContextFactory<LocalFantasyLeagueContext>>();
                dbContextFactory.Setup(f => f.CreateDbContext()).Returns(context);

                var deleteService = new DeleteService(dbContextFactory.Object);

                // Act
                await deleteService.DeleteAllCorrespondingMatchData(MatchId);
            }

            // Assert
            await using var assertionContext = new LocalFantasyLeagueContext(_dbContextOptions);
            Assert.Multiple(() =>
            {
                Assert.That(assertionContext.PerformanceStats.Where(ps => ps.MatchId == MatchId), Is.Empty);
                Assert.That(assertionContext.UserFantasySelections.Where(ufs => ufs.MatchId == MatchId), Is.Empty);
            });
        }
    }

}