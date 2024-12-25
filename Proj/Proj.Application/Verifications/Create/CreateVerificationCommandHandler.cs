using Common.Application;
using Proj.Domain.VerificationAgg;
using Proj.Domain.VerificationAgg.Repositories;

namespace Proj.Application.Verifications.Create;

internal class CreateVerificationCommandHandler : IBaseCommandHandler<CreateVerificationCommand,long>
{
    private readonly IVerificationRepository _repository;

    public CreateVerificationCommandHandler(IVerificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<long>> Handle(CreateVerificationCommand request, CancellationToken cancellationToken)
    {
        var verification = new Verification(request.UserIdentifier, request.VerificationCode, request.ExpirationTime, request.IsVerified);
        await _repository.AddAsync(verification);
        await _repository.Save();
        return OperationResult<long>.Success(verification.Id);
    }
}