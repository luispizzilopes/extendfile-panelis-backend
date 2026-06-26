using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.EnableTwoFactor;

public class EnableTwoFactorUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public EnableTwoFactorUseCase(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<EnableTwoFactorResponse>> ExecuteAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado");

        if (user.TwoFactorEnabled)
            return Error.Conflict("TwoFactor.AlreadyEnabled", "Autenticação de dois fatores já está habilitada");

        var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Error.Failure("TwoFactor.EnableFailed", $"Não foi possível habilitar o 2FA: {errors}");
        }

        return new EnableTwoFactorResponse
        {
            Success = true,
            Message = "Autenticação de dois fatores habilitada com sucesso"
        };
    }
}
