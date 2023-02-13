using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    }
}
