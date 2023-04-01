using Core.Entities.Photos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class PhotoGameConfiguration : IEntityTypeConfiguration<PhotoGame>
{
    public void Configure(EntityTypeBuilder<PhotoGame> builder)
    {
        builder.ToTable("PhotoGame");
        builder.Property(p => p.Url).HasMaxLength(250);
        builder.Property(p => p.PublicId).HasMaxLength(200);
    }
}