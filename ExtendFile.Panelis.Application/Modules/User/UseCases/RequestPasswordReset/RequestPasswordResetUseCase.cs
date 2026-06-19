using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Application.Modules.User.Requests.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.RequestPasswordReset;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.RequestPasswordReset;

public class RequestPasswordResetUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;
    private readonly IBrevoEmailService _emailService;
    private readonly IPasswordResetCodeStore _codeStore;

    public RequestPasswordResetUseCase(
        UserManager<Domain.Modules.User.Entities.User> userManager,
        IBrevoEmailService emailService,
        IPasswordResetCodeStore codeStore)
    {
        _userManager = userManager;
        _emailService = emailService;
        _codeStore = codeStore;
    }

    public async Task<ErrorOr<RequestPasswordResetResponse>> ExecuteAsync(
        RequestPasswordResetRequest request,
        CancellationToken cancellationToken)
    {
        var genericResponse = new RequestPasswordResetResponse
        {
            Success = true,
            Message = "Se o e-mail estiver cadastrado, você receberá um código de verificação em breve."
        };

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return genericResponse;

        var code = GenerateSixDigitCode();
        var identityToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _codeStore.StoreAsync(request.Email, code, identityToken, cancellationToken);

        var body = $@"
            <p>Você solicitou a recuperação da sua senha no sistema <strong>Panelis</strong>.</p>
            <p>Use o código abaixo para redefinir sua senha:</p>
            <h2 style=""letter-spacing:4px"">{code}</h2>
            <p>Este código é válido por <strong>15 minutos</strong>.</p>
            <p>Se você não solicitou a recuperação de senha, ignore este e-mail.</p>";

        try
        {
            await _emailService.SendAsync(
                request.Email,
                "Recuperação de Senha - Panelis",
                body,
                cancellationToken);
        }
        catch (Exception ex)
        {
            await _codeStore.RemoveAsync(request.Email, cancellationToken);
            return Error.Failure("Email.SendFailed", $"Não foi possível enviar o e-mail de recuperação. Detalhes: {ex.Message}");
        }

        return genericResponse;
    }

    private static string GenerateSixDigitCode()
    {
        return Random.Shared.Next(100000, 999999).ToString();
    }
}
