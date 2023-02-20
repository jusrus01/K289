using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourneyRent.Contracts;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Configurations
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = TourneyRentRoles.Administrator,
                NormalizedName = TourneyRentRoles.Administrator.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }
    }
}
