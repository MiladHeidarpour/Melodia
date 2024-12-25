using Common.Application;

namespace Proj.Application.Verifications.Delete;

public record DeleteVerificationCommand(long VerificationId) : IBaseCommand;