using Microsoft.AspNetCore.Authorization;
using TourneyRent.Contracts;

namespace TourneyRent.Authentication.Attributes
{
    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute()
        {
            Roles = TourneyRentRoles.Administrator;
        }
    }
}
