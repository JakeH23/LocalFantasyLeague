namespace LocalFantasyLeague.Models;

public class SeasonResult
{
    public int SeasonId { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public DateTime SeasonStartDate { get; set; }
    public DateTime SeasonEndDate { get; set; }
    public int TotalMatches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
}