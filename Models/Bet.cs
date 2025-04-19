using LocalFantasyLeague.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LocalFantasyLeague.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public BetType Type { get; set; }
        public int PlayerId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public bool IsResolved { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Payout { get; set; }
    }
}
