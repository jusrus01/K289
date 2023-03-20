using System.ComponentModel.DataAnnotations;

namespace TourneyRent.DataLayer.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
