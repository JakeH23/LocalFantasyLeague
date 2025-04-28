namespace LocalFantasyLeague.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;  // We'll hash the password for security
                                                                 
        // Navigation property for the associated Player
        public int? PlayerId { get; set; }  // Foreign key to Player
        public Player? Player { get; set; }

        public int? TeamId { get; set; }  // Foreign key to Team

        // Navigation property for the associated Team
        public Team? Team { get; set; }
    }
}
