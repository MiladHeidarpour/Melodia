using Common.Application;
using Proj.Domain.VerificationAgg.Repositories;

namespace Proj.Application.Verifications.Delete;

internal class DeleteVerificationCommandHandler : IBaseCommandHandler<DeleteVerificationCommand>
{
    private readonly IVerificationRepository _repository;

    public DeleteVerificationCommandHandler(IVerificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DeleteVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteVerification(request.VerificationId);
        if (result == false)
        {
            return OperationResult.Error("عملیات با شکست مواجه شد");
        }
        await _repository.Save();
        return OperationResult.Success("توکن با موفقیت حذف شد");
    }
}