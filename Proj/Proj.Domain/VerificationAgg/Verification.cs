using Common.Domain.Base;

namespace Proj.Domain.VerificationAgg;

public class Verification : AggregateRoot
{
    public string UserIdentifier { get; private set; }  // ایمیل یا شماره تلفن
    public string VerificationCode { get; private set; }
    public DateTime ExpirationTime { get; private set; }  // زمان انقضا
    public bool IsVerified { get; private set; }  // برای ذخیره‌سازی وضعیت تایید

    public Verification(string userIdentifier, string verificationCode, DateTime expirationTime, bool isVerified)
    {
        UserIdentifier = userIdentifier;
        VerificationCode = verificationCode;
        ExpirationTime = expirationTime;
        IsVerified = isVerified;
    }

    public void verify(bool isVerified)
    {
        IsVerified = isVerified;
    }
}