using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourneyRent.DataLayer.Models;

namespace TourneyRent.DataLayer.Configurations;

public class PrizeConfiguration : IEntityTypeConfiguration<Prize>
{
    public void Configure(EntityTypeBuilder<Prize> builder)
    {
        builder.HasData(new Prize
        {
            Id = Guid.NewGuid(),
            Name = "KOORUI 24.5 Inch FHD Gaming Monitor (used)",
            Description = "Gaming monitor",
        });
        
        builder.HasData(new Prize
        {
            Id = Guid.NewGuid(),
            Name = "Dell Latitude 3520 Laptop 15.6 (used)",
            Description = "Laptop",
        });
        
        builder.HasData(new Prize
        {
            Id = Guid.NewGuid(),
            Name = "DELL Latitude 5490 (used)",
            Description = "Gaming monitor",
        });
    }
}