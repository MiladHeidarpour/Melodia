using Common.Application;
using Proj.Application.Verifications.Create;
using Proj.Application.Verifications.Delete;
using Proj.Query.Verifications.Dtos;

namespace Proj.Presentation.Facade.Verifications;

public interface IVerificationFacade
{
    //Command
    Task<OperationResult<long>> Create(CreateVerificationCommand command);
    Task<OperationResult> Delete(DeleteVerificationCommand command);
    Task<OperationResult> VerifyCode(long verificationId);


    //Query
    Task<VerificationDto?> GetVerificationById(long verificationId);
}