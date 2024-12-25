using Common.Query;

namespace Proj.Query.Verifications.Dtos;

public class VerificationDto:BaseDto
{
    public string UserIdentifier { get; set; }
    public string VerificationCode { get; set; }
    public DateTime ExpirationTime { get; set; }
    public bool IsVerified { get; set; }
}