using System.ComponentModel.DataAnnotations;

namespace LocalFantasyLeague.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Please select a team.")]
        public int TeamId { get; set; }

        public int PlayerId { get; set; }
    }
}
