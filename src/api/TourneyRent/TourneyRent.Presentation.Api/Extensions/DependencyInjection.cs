using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TourneyRent.Authentication.Services;
using TourneyRent.BusinessLogic.Services;
using TourneyRent.DataLayer;
using TourneyRent.DataLayer.Models;
using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.Presentation.Api.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<PaymentTransactionRepository>();
            services.AddScoped<ImageRepository>();
            services.AddScoped<TournamentRepository>();
            services.AddScoped<TournamentService>();
            services.AddScoped<ImageService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<AccountService>();
            services.AddScoped<TeamRepository>();
            services.AddScoped<TeamService>();
            services.AddScoped<RentalItemRepository>();
            services.AddScoped<RentalItemService>();
		}

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Development settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<TourneyRentDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TourneyRentDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<TransactionExecutor>();
        }
    }
}
