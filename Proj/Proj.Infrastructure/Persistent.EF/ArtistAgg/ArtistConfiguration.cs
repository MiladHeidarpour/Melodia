using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.ArtistAgg;

namespace Proj.Infrastructure.Persistent.EF.ArtistAgg;

public class ArtistConfiguration:IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists", "dbo");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.ArtistName).IsRequired().HasMaxLength(150);
        builder.Property(c => c.ArtistImg).IsRequired();
        builder.Property(c => c.Slug).IsRequired().IsUnicode(false);

        builder.OwnsOne(b => b.SeoData, config =>
        {
            config.Property(b => b.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(b => b.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            config.Property(b => b.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            config.Property(b => b.IndexPage)
                .HasColumnName("IndexPage");

            config.Property(b => b.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            config.Property(b => b.Schema)
                .HasColumnName("Schema");
        });
    }
}