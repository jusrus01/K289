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
                Id = DemoAdminAccount.AdminRoleId,
                Name = TourneyRentRoles.Administrator,
                NormalizedName = TourneyRentRoles.Administrator.ToUpper(),
                ConcurrencyStamp = "3d887dc2-f10e-4b6a-be3b-4e36870d73f8"
            });
        }
    }
}
