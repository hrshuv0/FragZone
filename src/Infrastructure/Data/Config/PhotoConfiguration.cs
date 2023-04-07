using Core.Entities.Photos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photo");
        builder.Property(p => p.Url).HasMaxLength(250);
        builder.Property(p => p.PublicId).HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(250);
    }
}