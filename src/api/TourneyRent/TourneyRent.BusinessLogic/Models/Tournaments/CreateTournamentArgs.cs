using Microsoft.AspNetCore.Http;
using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models.Tournaments;

public class TournamentReservationArgs
{
    public int Id { get; set; }
    public IEnumerable<DateTime> Days { get; set; } = new List<DateTime>();
}

public class CreateTournamentArgs : IImageUpload, ITransactionable
{
    public Guid? PrizeId { get; set; }

    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float EntryFee { get; set; }
    public int ParticipantCount { get; set; }

    public IEnumerable<TournamentReservationArgs>? Reservation { get; set; }
    public IFormFile ImageFile { get; set; }

    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }
}