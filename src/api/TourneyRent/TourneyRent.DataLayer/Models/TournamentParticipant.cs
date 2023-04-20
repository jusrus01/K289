using System.ComponentModel.DataAnnotations;

namespace TourneyRent.DataLayer.Models
{
    public class TournamentParticipant
    {
        [Key]
        public int Id { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Transaction that is the same as the user
        /// </summary>
        public Guid? TransactionId { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}
