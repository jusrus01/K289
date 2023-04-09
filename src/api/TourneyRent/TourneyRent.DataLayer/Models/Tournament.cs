using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models;

public class Tournament : IImage, ITransactionable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float EntryFee { get; set; }
    public int ParticipantCount { get; set; }
    public Guid? ImageId { get; set; }

    public ApplicationUser Owner { get; set; }
    public string OwnerId { get; set; }

    /// <summary>
    /// Transaction id that should be the same as the user
    /// who is creating the tournament
    /// </summary>
    public Guid? TransactionId { get; set; }

    public ICollection<TournamentParticipant> Participants { get; set; }
    
    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }

    /// <summary>
    /// Making one-to-many
    /// in case we might want to extend this
    /// and allow adding prizes for different places e.g.
    /// 1st place prize, 2nd place and so on
    /// </summary>
    public ICollection<Prize> Prizes { get; set; }
    
    public bool IsWinnerSelected { get; set; }
}