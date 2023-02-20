using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Configurations
{
    internal static class DemoAdminAccount
    {
        public static string AdminRoleId = "bca7c0cf-de3b-441a-9a93-29667a913469";
        public static string AdminId = "2f2c8f88-466a-46a7-973e-2e846effdf24";
    }

    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var email = "admin@admin.com";
            builder.HasData(new ApplicationUser
            {
                Id = DemoAdminAccount.AdminId,
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                ConcurrencyStamp = "d9613afe-bf71-4011-ab09-10540da12750",
                PasswordHash = "AQAAAAEAACcQAAAAENlhkWeGcBe8J95nTzkIaLj9eD0pR7vaJ4+89LD2P0HMrraG2ZvpWiBn1+hpn9NA3A==", // admin,
                SecurityStamp = "37QVYJOLLDOSH5MWJSLBOWGDCAXPT2QA"
            });
        }
    }
}
