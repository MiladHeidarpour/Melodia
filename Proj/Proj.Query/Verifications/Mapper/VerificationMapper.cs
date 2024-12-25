using Proj.Domain.UserAgg;
using Proj.Domain.VerificationAgg;
using Proj.Query.Verifications.Dtos;

namespace Proj.Query.Verifications.Mapper;

public static class VerificationMapper
{
    public static VerificationDto MapToDto(this Verification verification)
    {
        return new VerificationDto()
        {
            Id = verification.Id,
            UserIdentifier = verification.UserIdentifier,
            VerificationCode = verification.VerificationCode,
            ExpirationTime = verification.ExpirationTime,
            IsVerified = verification.IsVerified,
            CreationDate = verification.CreationDate,
        };
    }
}