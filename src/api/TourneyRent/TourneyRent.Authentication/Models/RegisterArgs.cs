namespace TourneyRent.Authentication.Models
{
    public record RegisterArgs(
        string Email,
        string Password,
        string FirstName,
        string LastName);
}
