using Common.Application;

namespace Proj.Application.Verifications.VerifyCode;

public record VerifyCodeCommand(long VerificationId) : IBaseCommand;