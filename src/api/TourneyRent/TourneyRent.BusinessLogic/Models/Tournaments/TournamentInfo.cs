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
    public IEnumerable<TournamentParticipantInfo> Participants { get; set; }
}
