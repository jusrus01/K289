using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models.Tournaments;

public record TournamentInfo(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    float EntryFee,
    int ParticipantCount,
    Guid? ImageId) : IImage
{
    public Guid? ImageId { get; set; } = ImageId;
}
