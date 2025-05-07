namespace LocalFantasyLeague.Models
{
    public class FantasyScorer
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FantasyPoints { get; set; }
    }
}