namespace LocalFantasyLeague.Models;

public class PlayerStats
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Appearances { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int YellowCards { get; set; }
    public int RedCards { get; set; }
    public int PenaltySaves { get; set; }
    public int CleanSheets { get; set; }
    public int PenaltyMissed { get; set; }
}