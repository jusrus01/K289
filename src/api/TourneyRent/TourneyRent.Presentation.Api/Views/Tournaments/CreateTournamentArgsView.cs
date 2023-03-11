using System.ComponentModel.DataAnnotations;
using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.Tournaments;

public record CreateTournamentArgsView(
        [Required] string Name,
        [Required] DateTime? StartDate,
        [Required] DateTime? EndDate,
        [Required] float? EntryFee,
        [Required] int? ParticipantCount,
        IFormFile? ImageFile)
    : IImageUpload
{
    public IFormFile ImageFile { get; set; } = ImageFile;
}
