using Microsoft.EntityFrameworkCore;
using Proj.Domain.VerificationAgg;
using Proj.Domain.VerificationAgg.Repositories;
using Proj.Infrastructure._Utilities;

namespace Proj.Infrastructure.Persistent.EF.VerificationAgg;

public class VerificationRepository : BaseRepository<Verification>, IVerificationRepository
{
    public VerificationRepository(ProjContext context) : base(context)
    {
    }

    public async Task<bool> DeleteVerification(long verificationId)
    {
        var verification = await _context.Verifications.FirstOrDefaultAsync(f => f.Id == verificationId);

        if (verification == null)
        {
            return false;
        }

        _context.Verifications.Remove(verification);
        return true;
    }
}