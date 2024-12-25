using Common.Query;
using Proj.Query.Verifications.Dtos;

namespace Proj.Query.Verifications.GetById;

public record GetVerificationByIdQuery(long VerificationId) : IQuery<VerificationDto>;