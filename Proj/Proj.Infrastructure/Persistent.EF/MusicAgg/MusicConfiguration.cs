using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.MusicAgg;

namespace Proj.Infrastructure.Persistent.EF.MusicAgg;

public class MusicConfiguration : IEntityTypeConfiguration<Music>
{
    public void Configure(EntityTypeBuilder<Music> builder)
    {
        builder.ToTable("Musics", "dbo");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.TrackName).IsRequired().HasMaxLength(250);
        builder.Property(c => c.TrackFile).IsRequired();
        builder.Property(c => c.TrackTime).IsRequired();
        builder.Property(c => c.ReleaseDate).IsRequired();
        builder.Property(c => c.Slug).IsRequired().IsUnicode(false);

        builder.OwnsMany(b => b.ArtistMusics, option =>
        {
            option.ToTable("ArtistMusics", "Musics");
            option.Property(f => f.ArtistType).IsRequired();
        });

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