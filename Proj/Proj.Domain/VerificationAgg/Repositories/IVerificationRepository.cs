using Common.Domain.Repositories;

namespace Proj.Domain.VerificationAgg.Repositories;

public interface IVerificationRepository : IBaseRepository<Verification>
{
    Task<bool> DeleteVerification(long verificationId);
}