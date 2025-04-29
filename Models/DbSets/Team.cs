using System.ComponentModel.DataAnnotations;

namespace LocalFantasyLeague.Models.DbSets
{
    public class Team : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        public string? Name { get; set; }

        public List<Player> Players { get; set; } = [];

        public List<User> Users { get; set; } = []; // Navigation property for related users
    }
}
