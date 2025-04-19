namespace LocalFantasyLeague.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Kickoff { get; set; }
        public int HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public List<PerformanceStat> Stats { get; set; } = new();
    }
}
