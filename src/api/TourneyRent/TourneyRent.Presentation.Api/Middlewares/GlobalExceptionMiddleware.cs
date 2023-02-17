using Newtonsoft.Json;
using System.Security.Authentication;
using TourneyRent.BusinessLogic.Exceptions;

namespace TourneyRent.Presentation.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AuthenticationException e)
            {
                await HandleExceptionAsync(httpContext, e.Message, StatusCodes.Status400BadRequest);
            }
            catch (BaseDomainException e)
            {
                await HandleExceptionAsync(httpContext, e.Message, StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, "Internal server error", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, string errorMessage, int errorStatusCode, int? customStatusCode = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorStatusCode;

            var responseContent = new
            {
                ErrorMessage = errorMessage
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseContent));
        }
    }
}
