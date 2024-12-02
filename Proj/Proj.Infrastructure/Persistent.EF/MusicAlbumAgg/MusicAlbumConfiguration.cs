using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.MusicAlbumAgg;

namespace Proj.Infrastructure.Persistent.EF.MusicAlbumAgg;

public class MusicAlbumConfiguration:IEntityTypeConfiguration<MusicAlbum>
{
    public void Configure(EntityTypeBuilder<MusicAlbum> builder)
    {
        builder.ToTable("MusicAlbums", "dbo");
        builder.HasKey(x => x.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.AlbumName).HasMaxLength(100).IsRequired();
        builder.Property(c => c.CoverImg).IsRequired();
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.AlbumType).IsRequired();
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