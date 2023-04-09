using TourneyRent.BusinessLogic.Models.Prizes;

namespace TourneyRent.BusinessLogic.Models.Tournaments;

public class TournamentInfo
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float EntryFee { get; set; }
    public int ParticipantCount { get; set; }
    public Guid? ImageId { get; set; }
    public int Id { get; set; }
    public string OwnerId { get; set; }
    public bool IsJoined { get; set; }
    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }
    public IEnumerable<TournamentParticipantInfo> Participants { get; set; }

    public PrizeInfo Prize { get; set; }
}
