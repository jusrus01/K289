using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models
{
    public record UserProfile(
        string Email,
        string FirstName,
        string LastName,
        Guid? ImageId)
        : IImage
    {
        public Guid? ImageId { get; set; } = ImageId;
    }
}
