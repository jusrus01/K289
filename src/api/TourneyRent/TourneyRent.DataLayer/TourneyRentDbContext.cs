using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer
{
    public class TourneyRentDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public TourneyRentDbContext(DbContextOptions<TourneyRentDbContext> options)
            :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
