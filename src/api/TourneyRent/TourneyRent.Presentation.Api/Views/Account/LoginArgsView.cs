using System.ComponentModel.DataAnnotations;

namespace TourneyRent.Presentation.Api.Views.Account
{
    public record LoginArgsView(
        [Required] string Email,
        [Required] string Password);
}
