using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.Account;

public record ChangeMyProfileImageArgsView(IFormFile ImageFile) : IImageUpload
{
    public IFormFile ImageFile { get; set; } = ImageFile;
}
