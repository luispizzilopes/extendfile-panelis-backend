using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Requests.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.Responses.VerifyPasswordResetCode;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.VerifyPasswordResetCode;

public class VerifyPasswordResetCodeUseCase
{
    private readonly IPasswordResetCodeStore _codeStore;

    public VerifyPasswordResetCodeUseCase(IPasswordResetCodeStore codeStore)
    {
        _codeStore = codeStore;
    }

    public async Task<ErrorOr<VerifyPasswordResetCodeResponse>> ExecuteAsync(
        VerifyPasswordResetCodeRequest request,
        CancellationToken cancellationToken)
    {
        var entry = await _codeStore.GetAsync(request.Email, cancellationToken);

        if (entry is null || entry.Code != request.Code)
            return Error.Validation("PasswordReset.InvalidCode", "Código inválido ou expirado.");

        return new VerifyPasswordResetCodeResponse
        {
            Success = true,
            Message = "Código verificado com sucesso."
        };
    }
}
