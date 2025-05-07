using LocalFantasyLeague.Data;
using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeagueTests.Services.Util
{
    public static class TestDataSeeder
    {
        public static void SeedData(LocalFantasyLeagueContext context)
        {
            // Clear existing data
            context.Players.RemoveRange(context.Players);
            context.Teams.RemoveRange(context.Teams);
            context.Matches.RemoveRange(context.Matches);
            context.PerformanceStats.RemoveRange(context.PerformanceStats);
            context.Users.RemoveRange(context.Users);
            context.UserFantasySelections.RemoveRange(context.UserFantasySelections);
            context.Seasons.RemoveRange(context.Seasons);
            context.SaveChanges();

            // Add Teams
            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "Oswestry FC" },
                new Team { Id = 2, Name = "Los Perros" },
                new Team { Id = 3, Name = "TwoFourNine" },
                new Team { Id = 4, Name = "Sale FC" },
                new Team { Id = 5, Name = "Brow Boys" },
                new Team { Id = 6, Name = "Bin Slippers" },
                new Team { Id = 7, Name = "Stamford Rejects" },
                new Team { Id = 8, Name = "Points Match" }
            };
            context.Teams.AddRange(teams);

            // Add Players
            var players = new List<Player>
            {
                new Player { Id = 1, Name = "Jake Heaney", Position = "GK", TeamId = 1 },
                new Player { Id = 2, Name = "George Turner", Position = "FWD", TeamId = 1 },
                new Player { Id = 3, Name = "Harry Garthwaite", Position = "FWD", TeamId = 1 },
                new Player { Id = 4, Name = "David Price", Position = "DEF", TeamId = 1 },
                new Player { Id = 5, Name = "Andy Cooling", Position = "MID", TeamId = 1 },
                new Player { Id = 6, Name = "Luke Sams", Position = "MID", TeamId = 1 },
                new Player { Id = 7, Name = "Paul Durney", Position = "DEF", TeamId = 1 },
                new Player { Id = 8, Name = "Matt Turner", Position = "DEF", TeamId = 1 },
                new Player { Id = 9, Name = "Iain Beddow", Position = "MID", TeamId = 1 },
                new Player { Id = 10, Name = "Ed", Position = "FWD", TeamId = 1 },
                new Player { Id = 11, Name = "Ben Davies", Position = "FWD", TeamId = 1 },
                new Player { Id = 12, Name = "Joe Stockton", Position = "FWD", TeamId = 1 },
                new Player { Id = 13, Name = "Ringer", Position = "MID", TeamId = 1 }
            };
            context.Players.AddRange(players);

            // Add Users
            var users = new List<User>
            {
                new User { Id = 1, Username = "JakeH", PasswordHash = "7NcYcNGWMxapfjrDQIyYNa2M8PPBvHA1J8MCZVNPda4=", PlayerId = 1, TeamId = 1 },
                new User { Id = 2, Username = "MattT", PasswordHash = "7NcYcNGWMxapfjrDQIyYNa2M8PPBvHA1J8MCZVNPda4=", PlayerId = 8, TeamId = 1 }
            };
            context.Users.AddRange(users);

            // Add Seasons
            var seasons = new List<Season>
            {
                new Season { Id = 1, Name = "Season 1", StartDate = new DateTime(2025, 3, 13), EndDate = new DateTime(2025, 6, 21) },
                new Season { Id = 2, Name = "Season 2", StartDate = new DateTime(2025, 5, 2), EndDate = new DateTime(2030, 10, 1) }
            };
            context.Seasons.AddRange(seasons);

            // Add Matches
            var matches = new List<Match>
            {
                new Match { Id = 1, Kickoff = new DateTime(2025, 3, 13, 20, 30, 0), HomeTeamId = 4, AwayTeamId = 1, HomeTeamGoals = 0, AwayTeamGoals = 5, SeasonId = 1 },
                new Match { Id = 2, Kickoff = new DateTime(2025, 3, 20, 19, 0, 0), HomeTeamId = 6, AwayTeamId = 1, HomeTeamGoals = 1, AwayTeamGoals = 5, SeasonId = 1 },
                new Match { Id = 3, Kickoff = new DateTime(2025, 3, 27, 20, 30, 0), HomeTeamId = 1, AwayTeamId = 2, HomeTeamGoals = 0, AwayTeamGoals = 5, SeasonId = 1 },
                new Match { Id = 4, Kickoff = new DateTime(2025, 4, 3, 19, 30, 0), HomeTeamId = 5, AwayTeamId = 1, HomeTeamGoals = 1, AwayTeamGoals = 0, SeasonId = 1 },
                new Match { Id = 5, Kickoff = new DateTime(2025, 4, 10, 20, 0, 0), HomeTeamId = 1, AwayTeamId = 3, HomeTeamGoals = 2, AwayTeamGoals = 3, SeasonId = 1 },
                new Match { Id = 6, Kickoff = new DateTime(2025, 4, 17, 20, 0, 0), HomeTeamId = 8, AwayTeamId = 1, HomeTeamGoals = 0, AwayTeamGoals = 5, SeasonId = 1 },
                new Match { Id = 7, Kickoff = new DateTime(2025, 4, 24, 19, 0, 0), HomeTeamId = 1, AwayTeamId = 7, HomeTeamGoals = 3, AwayTeamGoals = 0, SeasonId = 1 },
                new Match { Id = 8, Kickoff = new DateTime(2025, 4, 24, 19, 30, 0), HomeTeamId = 1, AwayTeamId = 6, HomeTeamGoals = 1, AwayTeamGoals = 0, SeasonId = 1 },
                new Match { Id = 9, Kickoff = new DateTime(2025, 5, 1, 20, 30, 0), HomeTeamId = 1, AwayTeamId = 4, HomeTeamGoals = 0, AwayTeamGoals = 0, SeasonId = 1 },
                new Match { Id = 10, Kickoff = DateTime.Now.AddDays(3), HomeTeamId = 1, AwayTeamId = 4, HomeTeamGoals = 0, AwayTeamGoals = 0, SeasonId = 2 }
            };
            context.Matches.AddRange(matches);

            // Add Performance Stats
            var performanceStats = new List<PerformanceStat>
{
    new PerformanceStat { Id = 1, MatchId = 1, PlayerId = 1, Appearance = true, Goals = 0, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 2, MatchId = 1, PlayerId = 2, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 3, MatchId = 1, PlayerId = 3, Appearance = true, Goals = 1, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 4, MatchId = 1, PlayerId = 4, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 5, MatchId = 1, PlayerId = 5, Appearance = true, Goals = 2, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 6, MatchId = 1, PlayerId = 6, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 7, MatchId = 1, PlayerId = 7, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 8, MatchId = 1, PlayerId = 8, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 9, MatchId = 1, PlayerId = 9, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 10, MatchId = 1, PlayerId = 10, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 11, MatchId = 1, PlayerId = 11, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 12, MatchId = 1, PlayerId = 12, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 13, MatchId = 1, PlayerId = 13, Appearance = true, Goals = 0, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 14, MatchId = 2, PlayerId = 1, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 15, MatchId = 2, PlayerId = 2, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 16, MatchId = 2, PlayerId = 3, Appearance = true, Goals = 2, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 17, MatchId = 2, PlayerId = 4, Appearance = true, Goals = 1, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 18, MatchId = 2, PlayerId = 5, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 19, MatchId = 2, PlayerId = 6, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 20, MatchId = 2, PlayerId = 7, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 21, MatchId = 2, PlayerId = 8, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 22, MatchId = 2, PlayerId = 9, Appearance = true, Goals = 1, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 23, MatchId = 2, PlayerId = 10, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 24, MatchId = 2, PlayerId = 11, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 25, MatchId = 2, PlayerId = 12, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 26, MatchId = 2, PlayerId = 13, Appearance = true, Goals = 1, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 27, MatchId = 3, PlayerId = 1, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 28, MatchId = 3, PlayerId = 2, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 29, MatchId = 3, PlayerId = 3, Appearance = true, Goals = 0, Assists = 1, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 30, MatchId = 3, PlayerId = 4, Appearance = true, Goals = 1, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 31, MatchId = 3, PlayerId = 5, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 32, MatchId = 3, PlayerId = 6, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 33, MatchId = 3, PlayerId = 7, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 34, MatchId = 3, PlayerId = 8, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 35, MatchId = 3, PlayerId = 9, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 36, MatchId = 3, PlayerId = 10, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 37, MatchId = 3, PlayerId = 11, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 38, MatchId = 3, PlayerId = 12, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 39, MatchId = 3, PlayerId = 13, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 40, MatchId = 4, PlayerId = 1, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 41, MatchId = 4, PlayerId = 2, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 42, MatchId = 4, PlayerId = 3, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 43, MatchId = 4, PlayerId = 4, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 44, MatchId = 4, PlayerId = 5, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 45, MatchId = 4, PlayerId = 6, Appearance = true, Goals = 0, Assists = 0, YellowCard = true, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 46, MatchId = 4, PlayerId = 7, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 47, MatchId = 4, PlayerId = 8, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 48, MatchId = 4, PlayerId = 9, Appearance = true, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 49, MatchId = 4, PlayerId = 10, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 },
    new PerformanceStat { Id = 50, MatchId = 4, PlayerId = 11, Appearance = false, Goals = 0, Assists = 0, YellowCard = false, RedCard = false, PenaltySaves = 0, CleanSheet = false, PenaltyMissed = 0 }
};
            context.PerformanceStats.AddRange(performanceStats);

            // Add User Fantasy Selections
            var userFantasySelections = new List<UserFantasySelection>
            {
                new UserFantasySelection { Id = 1, MatchId = 1, UserId = 1, Players = new List<int> { 1, 3, 4 }, CaptainedPlayerId = 1 },
                new UserFantasySelection { Id = 2, MatchId = 2, UserId = 1, Players = new List<int> { 5, 9, 1 }, CaptainedPlayerId = null }
            };
            context.UserFantasySelections.AddRange(userFantasySelections);

            // Save changes
            context.SaveChanges();
        }
    }
}
