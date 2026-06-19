using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Requests.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.ConfirmPasswordReset;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.ConfirmPasswordReset;

public class ConfirmPasswordResetUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;
    private readonly IPasswordResetCodeStore _codeStore;

    public ConfirmPasswordResetUseCase(
        UserManager<Domain.Modules.User.Entities.User> userManager,
        IPasswordResetCodeStore codeStore)
    {
        _userManager = userManager;
        _codeStore = codeStore;
    }

    public async Task<ErrorOr<ConfirmPasswordResetResponse>> ExecuteAsync(
        ConfirmPasswordResetRequest request,
        CancellationToken cancellationToken)
    {
        var entry = await _codeStore.GetAsync(request.Email, cancellationToken);

        if (entry is null || entry.Code != request.Code)
            return Error.Validation("PasswordReset.InvalidCode", "Código inválido ou expirado.");

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado.");

        var result = await _userManager.ResetPasswordAsync(user, entry.IdentityToken, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Error.Failure("PasswordReset.Failed", string.Join(", ", errors));
        }

        await _codeStore.RemoveAsync(request.Email, cancellationToken);

        return new ConfirmPasswordResetResponse
        {
            Success = true,
            Message = "Senha redefinida com sucesso."
        };
    }
}
