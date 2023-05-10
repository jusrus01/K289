using System.ComponentModel.DataAnnotations;

namespace TourneyRent.Presentation.Api.Views.Tournaments;

public class TournamentReservationArgsView
{
    public int Id { get; set; }
    public List<DateTime> Days { get; set; } = new();
}

public class CreateTournamentArgsView
{
    public IFormFile? ImageFile { get; set; }

    public string? BankAccountName { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? TransactionReason { get; set; }

    public Guid? PrizeId { get; set; }

    [Required] public string Name { get; set; }

    [Required] public DateTime? StartDate { get; set; }

    [Required] public DateTime? EndDate { get; set; }

    [Required] public float? EntryFee { get; set; }

    [Required] public int? ParticipantCount { get; set; }


    public string Reservation { get; set; } = "[]"; // Does not map to defined structure from form-data
}