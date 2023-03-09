namespace TourneyRent.Authentication.Models
{
    public record UserInfo(string userId, IList<string> Roles);
}
