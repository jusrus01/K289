using Microsoft.AspNetCore.Identity;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models
{
    public class ApplicationUser : IdentityUser, IImage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid? ImageId { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
        /// <summary>
        /// All instances of tournaments participantion of this user
        /// </summary>
        public ICollection<TournamentParticipant> Participation { get; set; } 
    }
}
