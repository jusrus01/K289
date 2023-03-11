using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
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

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }
        
        public DbSet<Tournament> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(m => m.Team)
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
