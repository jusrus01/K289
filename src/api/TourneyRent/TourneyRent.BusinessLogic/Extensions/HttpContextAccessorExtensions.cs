using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TourneyRent.BusinessLogic.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetAuthenticatorUserId(this IHttpContextAccessor context)
        {
            var userClaims = context.HttpContext.User.Identity as ClaimsIdentity;
            return userClaims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

    }
}
