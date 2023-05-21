using System.ComponentModel.DataAnnotations;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models;

public class RentalItem : IImage, ITransactionable
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public ApplicationUser Owner { get; set; }
    public string OwnerId { get; set; }

    public ICollection<CalendarIRentalItemEntry> AvailableDays { get; set; }

    public Guid? ImageId { get; set; }

    public decimal HighlightFee { get; set; }

    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }
}