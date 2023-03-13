using Microsoft.AspNetCore.Http;
using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models.Tournaments;

public record CreateTournamentArgs(
        string Name,
        DateTime StartDate,
        DateTime EndDate,
        float EntryFee,
        int ParticipantCount,
        IFormFile ImageFile)
    : IImageUpload
{
    public IFormFile ImageFile { get; set; } = ImageFile;
}
