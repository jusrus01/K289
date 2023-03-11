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
}