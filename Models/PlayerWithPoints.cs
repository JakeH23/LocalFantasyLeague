namespace LocalFantasyLeague.Models
{
    public class PlayerWithPoints
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public int? TeamId { get; set; }
        public int Appearances { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int PenaltySaves { get; set; }
        public int CleanSheets { get; set; }
        public int PenaltyMissed { get; set; }
        public int TotalPoints { get; set; }
    }
}
