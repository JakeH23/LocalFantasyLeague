using LocalFantasyLeague.Data;
using LocalFantasyLeague.Services.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace LocalFantasyLeagueTests.Services.Util
{
    public abstract class BaseServiceTest<TService> where TService : class
    {
        protected LocalFantasyLeagueContext Context;
        protected ILogger<TService> Logger;
        protected IComponentService<TService> ComponentService;

        [SetUp]
        public virtual void SetUp()
        {
            var options = new DbContextOptionsBuilder<LocalFantasyLeagueContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            Context = new LocalFantasyLeagueContext(options);
            TestDataSeeder.SeedData(Context);

            using var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            Logger = loggerFactory.CreateLogger<TService>();

            var dbContextFactory = new Mock<IDbContextFactory<LocalFantasyLeagueContext>>();
            dbContextFactory.Setup(f => f.CreateDbContext()).Returns(Context);

            ComponentService = new ComponentService<TService>(dbContextFactory.Object, Logger);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Context.Dispose();
        }
    }
}