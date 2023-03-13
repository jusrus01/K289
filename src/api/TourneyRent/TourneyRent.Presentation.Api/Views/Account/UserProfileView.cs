using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.Account
{
    public record UserProfileView(
        string FirstName,
        string LastName,
        Guid? ImageId)
        : IImage
    {
        public Guid? ImageId { get; set; } = ImageId;
    }
}
