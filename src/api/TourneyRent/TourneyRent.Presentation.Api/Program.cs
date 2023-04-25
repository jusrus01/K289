using AutoMapper;
using System.Reflection;
using System.Text.Json.Serialization;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.Contracts.Options;
using TourneyRent.DataLayer;
using TourneyRent.Presentation.Api.Controllers;
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
                .Select(profile => (Profile)Activator.CreateInstance(profile))
                .ToList();
            builder.Services.AddAutoMapper(cfg =>
                cfg.AddProfiles(profiles));

            builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("MailConfiguration"));
            
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
            builder.Services
                .AddControllers(options =>
                    options.Filters.Add<GlobalModelStateValidationFilter>()).AddJsonOptions(opt =>
                    {
                        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            builder.Services.AddScoped<TeamService>();

            var app = builder.Build();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "DefaultApi",
                    pattern: "{controller}/{action}/{id?}",
                    // ReSharper disable once Mvc.ControllerNotResolved
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