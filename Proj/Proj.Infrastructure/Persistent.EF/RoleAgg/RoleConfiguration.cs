using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.RoleAgg;

namespace Proj.Infrastructure.Persistent.EF.RoleAgg;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "dbo");
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(60);

        builder.OwnsMany(b => b.Permissions, option =>
        {
            option.ToTable("Permissions", "Role");
        });
    }
}