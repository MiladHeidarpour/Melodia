using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.UserAgg;

namespace Proj.Infrastructure.Persistent.EF.UserAgg;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");
        builder.HasKey(c => c.Id);
        builder.HasIndex(b => b.PhoneNumber).IsUnique();
        //builder.HasIndex(b => b.Email).IsUnique();

        builder.Property(c => c.RoleId).IsRequired();
        builder.Property(b => b.FullName).IsRequired(false).HasMaxLength(100);
        builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(11);
        builder.Property(b => b.Email).IsRequired(false).HasMaxLength(256);
        builder.Property(b => b.Password).IsRequired().HasMaxLength(50);

        builder.OwnsMany(b => b.Tokens, option =>
        {
            option.ToTable("Tokens", "User");
            option.HasKey(b => b.Id);

            option.Property(b => b.HashJwtToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.HashRefreshToken)
                .IsRequired()
                .HasMaxLength(250);

            option.Property(b => b.Device)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}