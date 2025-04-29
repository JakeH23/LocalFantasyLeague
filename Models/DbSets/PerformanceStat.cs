namespace LocalFantasyLeague.Models.DbSets
{
    public class PerformanceStat : IEntity
    {
        public int Id { get; set; }

        // Foreign key to Match
        public int MatchId { get; set; }
        public Match? Match { get; set; }

        // Foreign key to Player
        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        // Stats for the player in the match
        public bool Appearance { get; set; }
        public int Goals { get; set; }

        public int Assists { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
        public int PenaltySaves { get; set; }
        public bool CleanSheet { get; set; }
        public int PenaltyMissed { get; set; }
    }
}
