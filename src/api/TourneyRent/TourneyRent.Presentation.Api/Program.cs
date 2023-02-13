using AutoMapper;
using System.Reflection;
using TourneyRent.Presentation.Api.Extensions;
using TourneyRent.Presentation.Api.Filters;
using TourneyRent.Presentation.Api.Middlewares;

namespace TourneyRent.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var assembly = Assembly.GetExecutingAssembly();
            var profiles = assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Profile)))
                .ToList();
            builder.Services.AddAutoMapper(cfg =>
                cfg.AddProfiles((IEnumerable<Profile>)profiles));

            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isAllowed => true)));

            builder.Services.ConfigureDatabase(builder.Configuration);
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureServices();
            builder.Services.AddControllers(options =>
                options.Filters.Add<GlobalModelStateValidationFilter>());

            var app = builder.Build();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "DefaultApi",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Default", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "errors",
                    pattern: "error/{action}/",
                    defaults: new { action = "NotFound" });
            });
            app.Run();
        }
    }
}