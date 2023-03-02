using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(x => x.Players)
                .WithOne(x => x.Team)
                .IsRequired(false);
        }
    }
}
