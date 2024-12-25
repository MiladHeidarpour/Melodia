using Common.Application;
using Proj.Domain.VerificationAgg.Repositories;

namespace Proj.Application.Verifications.VerifyCode;

internal class VerifyCodeCommandHandler : IBaseCommandHandler<VerifyCodeCommand>
{
    private readonly IVerificationRepository _repository;

    public VerifyCodeCommandHandler(IVerificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
    {
        var verification = await _repository.GetTracking(request.VerificationId);
        if (verification == null)
        {
            return OperationResult.NotFound("کد نامعتبر است");
        }

        verification.verify(true);
        await _repository.Save();
        return OperationResult.Success("کد با موفقیت تایید شد");
    }
}