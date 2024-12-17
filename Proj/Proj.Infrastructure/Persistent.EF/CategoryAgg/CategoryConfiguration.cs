using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.CategoryAgg;

namespace Proj.Infrastructure.Persistent.EF.CategoryAgg;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "dbo");

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.Slug).IsRequired().IsUnicode(false);
        builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

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