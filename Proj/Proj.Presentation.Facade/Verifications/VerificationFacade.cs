using Common.Application;
using MediatR;
using Proj.Application.Verifications.Create;
using Proj.Application.Verifications.Delete;
using Proj.Application.Verifications.VerifyCode;
using Proj.Query.Verifications.Dtos;
using Proj.Query.Verifications.GetById;

namespace Proj.Presentation.Facade.Verifications;

public class VerificationFacade : IVerificationFacade
{
    private readonly IMediator _mediator;

    public VerificationFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult<long>> Create(CreateVerificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteVerificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> VerifyCode(long verificationId)
    {
        return await _mediator.Send(new VerifyCodeCommand(verificationId));
    }

    public async Task<VerificationDto?> GetVerificationById(long verificationId)
    {
        return await _mediator.Send(new GetVerificationByIdQuery(verificationId));
    }
}