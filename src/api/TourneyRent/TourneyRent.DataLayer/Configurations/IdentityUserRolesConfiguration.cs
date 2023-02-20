using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourneyRent.DataLayer.Configurations
{
    internal class IdentityUserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                RoleId = DemoAdminAccount.AdminRoleId,
                UserId = DemoAdminAccount.AdminId
            });
        }
    }
}
