using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models;

public class Tournament : IImage
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
}