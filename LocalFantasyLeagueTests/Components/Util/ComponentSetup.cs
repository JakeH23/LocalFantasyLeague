using LocalFantasyLeague.Models;
using LocalFantasyLeague.Models.DbSets;
using LocalFantasyLeague.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Match = LocalFantasyLeague.Models.DbSets.Match;

namespace LocalFantasyLeagueTests.Components.Util
{
    public class ComponentSetup : Bunit.TestContext
    {
        public Mock<IMatchService> _mockMatchService;
        public Mock<IPerformanceStatService> _mockPerformanceStatService;
        public Mock<IPlayerService> _mockPlayerService;
        public Mock<ISeasonService> _mockSeasonService;
        public Mock<ITeamService> _mockTeamService;
        public Mock<IUserFantasySelectionService> _mockUserFantasySelectionService;
        public Mock<IDataService> _mockDataService;
        public Mock<IUserService> _mockUserService;
        public Mock<UserSession> _mockUserSession;

        private List<Season> _mockSeasons;
        private List<User> _mockUsers;
        private List<UserFantasySelection> _mockSelections;
        private List<Match> _mockMatches;
        private List<PerformanceStat> _mockPerformanceStats;
        private List<Team> _mockTeams;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockMatchService = new Mock<IMatchService>();
            _mockPerformanceStatService = new Mock<IPerformanceStatService>();
            _mockPlayerService = new Mock<IPlayerService>();
            _mockSeasonService = new Mock<ISeasonService>();
            _mockTeamService = new Mock<ITeamService>();
            _mockUserFantasySelectionService = new Mock<IUserFantasySelectionService>();
            _mockDataService = new Mock<IDataService>();
            _mockUserService = new Mock<IUserService>();
            _mockUserSession = new Mock<UserSession>();

            // Mock data
            _mockMatches = new List<Match>
            {
                new Match {
                    Id = 1,
                    Kickoff = new DateTime(2024, 8, 2),
                    HomeTeamId = 1,
                    HomeTeam = new Team { Id = 1, Name = "Team A" },
                    HomeTeamGoals = 2,
                    AwayTeamId = 2,
                    AwayTeam = new Team { Id = 2, Name = "Team B" },
                    AwayTeamGoals = 1,
                    SeasonId = 1
                }
            };

            _mockTeams = new List<Team>
            {
                new Team { Id = 1, Name = "Team A", Players = new List<Player>{ new Player { Id = 101 }}, Users = _mockUsers},
                new Team { Id = 2, Name = "Team B", Players = new List<Player>(), Users = new List<User>()}
            };

            _mockSeasons = new List<Season>
            {
                new Season { Id = 1, Name = "2024/2025", StartDate = new DateTime(2024, 8, 1), EndDate = new DateTime(2025, 5, 31) }
            };

            _mockUsers = new List<User>
            {
                new User { Id = 1, Username = "User1" },
                new User { Id = 2, Username = "User2" }
            };

            _mockSelections = new List<UserFantasySelection>
            {
                new UserFantasySelection { UserId = 1, MatchId = 1, Players = new List<int> { 101, 102 }, CaptainedPlayerId = 101 },
                new UserFantasySelection { UserId = 2, MatchId = 1, Players = new List<int> { 103, 104 }, CaptainedPlayerId = 103 }
            };

            _mockPerformanceStats = new List<PerformanceStat>
            {
                new PerformanceStat { MatchId = 1, PlayerId = 101, Appearance = true, Goals = 1, Assists = 2 },
                new PerformanceStat { MatchId = 1, PlayerId = 102, Appearance = true, Goals = 0, Assists = 0 },
                new PerformanceStat { MatchId = 1, PlayerId = 103, Appearance = true, Goals = 3, Assists = 1 },
                new PerformanceStat { MatchId = 1, PlayerId = 104, Appearance = true, Goals = 2, Assists = 3 }
            };

            _mockMatchService.Setup(s => s.GetMatchListIncludingTeamsAndSeason()).ReturnsAsync(_mockMatches);
            _mockDataService.Setup(ds => ds.GetAllAsync<Season>()).ReturnsAsync(_mockSeasons);
            _mockDataService.Setup(ds => ds.GetAllAsync<User>()).ReturnsAsync(_mockUsers);
            _mockDataService.Setup(ds => ds.GetAllAsync<UserFantasySelection>()).ReturnsAsync(_mockSelections);
            _mockDataService.Setup(ds => ds.GetAllAsync<Match>()).ReturnsAsync(_mockMatches);
            _mockDataService.Setup(ds => ds.GetAllAsync<PerformanceStat>()).ReturnsAsync(_mockPerformanceStats);
            _mockDataService.Setup(ds => ds.GetAllAsync<Team>()).ReturnsAsync(_mockTeams);

            // Mock CurrentUser in UserSession
            var mockUser = new User { Id = 1, Username = "TestUser", TeamId = 1, PlayerId = 101 };
            _mockUserSession.SetupProperty(us => us.CurrentUser, mockUser);

            // Register services
            Services.AddScoped(x => _mockUserService.Object);
            Services.AddScoped(x => _mockDataService.Object);
            Services.AddScoped(x => _mockMatchService.Object);
            Services.AddScoped(x => _mockPerformanceStatService.Object);
            Services.AddScoped(x => _mockPlayerService.Object);
            Services.AddScoped(x => _mockSeasonService.Object);
            Services.AddScoped(x => _mockTeamService.Object);
            Services.AddScoped(x => _mockUserFantasySelectionService.Object);
            Services.AddSingleton(_mockUserSession.Object);
        }
    }
}
