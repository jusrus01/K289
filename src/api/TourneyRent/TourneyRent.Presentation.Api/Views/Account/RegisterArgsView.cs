using System.ComponentModel.DataAnnotations;

namespace TourneyRent.Presentation.Api.Views.Account
{
    public record RegisterArgsView(
        [Required] string Email,
        [Required] string Password,
        [Required] string FirstName,
        [Required] string LastName);
}
