using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj.Domain.VerificationAgg;

namespace Proj.Infrastructure.Persistent.EF.VerificationAgg;

public class VerificationConfiguration: IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.ToTable("Verifications", "dbo");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserIdentifier).IsRequired();
        builder.Property(c => c.VerificationCode).IsRequired();
        builder.Property(c => c.ExpirationTime).IsRequired();
        builder.Property(c => c.IsVerified).IsRequired();
    }
}