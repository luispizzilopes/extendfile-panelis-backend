using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Requests.TwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.VerifyTwoFactor;

public class VerifyTwoFactorUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;
    private readonly ITwoFactorCodeStore _codeStore;
    private readonly ITokenJwtService _tokenJwtService;

    public VerifyTwoFactorUseCase(
        UserManager<Domain.Modules.User.Entities.User> userManager,
        ITwoFactorCodeStore codeStore,
        ITokenJwtService tokenJwtService)
    {
        _userManager = userManager;
        _codeStore = codeStore;
        _tokenJwtService = tokenJwtService;
    }

    public async Task<ErrorOr<UserSessionResponse>> ExecuteAsync(
        VerifyTwoFactorRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado");

        if (!user.Active.HasValue || !user.Active.Value)
            return Error.Validation("User.Inactive", "Usuário está inativo");

        var storedCode = await _codeStore.GetAsync(request.Email, cancellationToken);
        if (storedCode is null)
            return Error.Validation("TwoFactor.CodeExpired", "Código expirado ou inválido. Faça login novamente");

        if (storedCode != request.Code)
            return Error.Validation("TwoFactor.InvalidCode", "Código inválido");

        await _codeStore.RemoveAsync(request.Email, cancellationToken);

        user.LastLoginAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var tokenJwtInformation = _tokenJwtService.CreateTokenUser(user);

        return new UserSessionResponse(
            user.Id,
            user.Email,
            user.Name,
            tokenJwtInformation,
            user.TwoFactorEnabled);
    }
}
