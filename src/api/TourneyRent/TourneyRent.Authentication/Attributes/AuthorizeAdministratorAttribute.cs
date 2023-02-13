using Microsoft.AspNetCore.Authorization;

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
