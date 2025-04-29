namespace LocalFantasyLeague.Models.DbSets
{
    public class UserFantasySelection : IEntity
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int? UserId { get; set; } // Foreign key to User
        public List<int> Players { get; set; } = []; // List of player IDs selected by the user
        public int? CaptainedPlayerId { get; set; } // Indicates if the player is a captain
    }
}