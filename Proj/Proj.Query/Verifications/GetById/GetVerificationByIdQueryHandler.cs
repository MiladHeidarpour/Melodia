using Common.Query;
using Microsoft.EntityFrameworkCore;
using Proj.Infrastructure.Persistent.EF;
using Proj.Query.Verifications.Dtos;
using Proj.Query.Verifications.Mapper;

namespace Proj.Query.Verifications.GetById;

internal class GetVerificationByIdQueryHandler : IQueryHandler<GetVerificationByIdQuery, VerificationDto>
{
    private readonly ProjContext _context;

    public GetVerificationByIdQueryHandler(ProjContext context)
    {
        _context = context;
    }
    public async Task<VerificationDto> Handle(GetVerificationByIdQuery request, CancellationToken cancellationToken)
    {
        var verification = await _context.Verifications.FirstOrDefaultAsync(f => f.Id == request.VerificationId, cancellationToken);
        if (verification == null)
        {
            return null;

        }
        return verification.MapToDto();
    }
}