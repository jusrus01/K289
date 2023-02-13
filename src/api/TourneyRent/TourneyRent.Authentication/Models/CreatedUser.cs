namespace TourneyRent.Authentication.Models
{
    public record class CreatedUser(
        string Email,
        string FirstName, 
        string LastName);
}
