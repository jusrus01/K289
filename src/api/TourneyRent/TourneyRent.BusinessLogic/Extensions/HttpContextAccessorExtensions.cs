using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TourneyRent.BusinessLogic.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetAuthenticatedUserId(this IHttpContextAccessor context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
