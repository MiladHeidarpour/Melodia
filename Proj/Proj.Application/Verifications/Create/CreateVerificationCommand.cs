using Common.Application;

namespace Proj.Application.Verifications.Create;

public class CreateVerificationCommand : IBaseCommand<long>
{
    public string UserIdentifier { get; set; }
    public string VerificationCode { get; set; }
    public DateTime ExpirationTime { get; set; }
    public bool IsVerified { get; set; }
}