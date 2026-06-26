using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Requests.Login;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.Login;

public class LoginUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;
    private readonly SignInManager<Domain.Modules.User.Entities.User> _signInManager;
    private readonly ITokenJwtService _tokenJwtService;
    private readonly IBrevoEmailService _emailService;
    private readonly ITwoFactorCodeStore _twoFactorCodeStore;

    public LoginUseCase(
        UserManager<Domain.Modules.User.Entities.User> userManager,
        SignInManager<Domain.Modules.User.Entities.User> signInManager,
        ITokenJwtService tokenJwtService,
        IBrevoEmailService emailService,
        ITwoFactorCodeStore twoFactorCodeStore)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenJwtService = tokenJwtService;
        _emailService = emailService;
        _twoFactorCodeStore = twoFactorCodeStore;
    }

    public async Task<ErrorOr<UserSessionResponse>> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado");

        if (!user.Active.HasValue || !user.Active.Value)
            return Error.Validation("User.Inactive", "Usuário está inativo");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return Error.Validation("Login.InvalidCredentials", "Email ou senha inválidos");

        if (user.TwoFactorEnabled)
        {
            var code = Random.Shared.Next(100000, 999999).ToString();
            await _twoFactorCodeStore.StoreAsync(user.Email!, code, cancellationToken);

            var body = $@"
                <p>Um novo acesso foi solicitado para sua conta no sistema <strong>Panelis</strong>.</p>
                <p>Use o código abaixo para concluir o login:</p>
                <h2 style=""letter-spacing:4px"">{code}</h2>
                <p>Este código é válido por <strong>5 minutos</strong>.</p>
                <p>Se você não solicitou este acesso, ignore este e-mail.</p>";

            try
            {
                await _emailService.SendAsync(
                    user.Email!,
                    "Código de Verificação - Panelis",
                    body,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                await _twoFactorCodeStore.RemoveAsync(user.Email!, cancellationToken);
                return Error.Failure("Email.SendFailed", $"Não foi possível enviar o código de verificação. Detalhes: {ex.Message}");
            }

            return new UserSessionResponse { Email = user.Email, RequiresTwoFactor = true };
        }

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
