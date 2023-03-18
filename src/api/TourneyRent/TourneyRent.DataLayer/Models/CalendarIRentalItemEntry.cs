using System.ComponentModel.DataAnnotations;

namespace TourneyRent.DataLayer.Models
{
    public class CalendarIRentalItemEntry
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }
        public RentalItem Item { get; set; }

        public decimal Price { get; set; }
        public DateTime AvailableAt { get; set; }

        public string BuyerId { get; set; }
        public ApplicationUser Buyer { get; set; }

        /// <summary>
        /// Should be the same as Buyers transaction id e.g.
        /// when buying this available day the buyer makes a transaction
        /// that should have matching ids.
        /// </summary>
        public Guid? TransactionId { get; set; }
    }
}
