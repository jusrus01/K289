using System.ComponentModel.DataAnnotations;
using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.Account
{
    public record UpdateUserProfileArgsView(
        [Required] string Id,
        [Required] string FirstName,
        [Required] string LastName,
        [Required] IFormFile ImageFile)
        :
        IImageUpload
    {
        public IFormFile ImageFile { get; set; } = ImageFile;
    }
}
