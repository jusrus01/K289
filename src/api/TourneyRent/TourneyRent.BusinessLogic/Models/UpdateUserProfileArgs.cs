using Microsoft.AspNetCore.Http;
using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models
{
    public record UpdateUserProfileArgs(
        string Id,
        string FirstName,
        string LastName);
}
