using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.DisableTwoFactor;

public class DisableTwoFactorUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public DisableTwoFactorUseCase(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<DisableTwoFactorResponse>> ExecuteAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado");

        if (!user.TwoFactorEnabled)
            return Error.Conflict("TwoFactor.AlreadyDisabled", "Autenticação de dois fatores já está desabilitada");

        var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Error.Failure("TwoFactor.DisableFailed", $"Não foi possível desabilitar o 2FA: {errors}");
        }

        return new DisableTwoFactorResponse
        {
            Success = true,
            Message = "Autenticação de dois fatores desabilitada com sucesso"
        };
    }
}
