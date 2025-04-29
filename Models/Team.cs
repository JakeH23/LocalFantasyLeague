using System.ComponentModel.DataAnnotations;

namespace LocalFantasyLeague.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        public string? Name { get; set; }

        public List<Player> Players { get; set; } = new();

        public List<User> Users { get; set; } = new(); // Navigation property for related users
    }
}
