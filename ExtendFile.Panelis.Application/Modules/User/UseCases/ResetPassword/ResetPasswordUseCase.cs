using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.ResetPassword;
using ExtendFile.Panelis.Application.Modules.User.Responses.ResetPassword;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.ResetPassword;

public class ResetPasswordUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public ResetPasswordUseCase(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<ResetPasswordResponse>> ExecuteAsync(
        ResetPasswordRequest request, 
        string email, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado.");

        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        
        if (!passwordCheckResult)
            return Error.Validation("User.InvalidCurrentPassword", "A senha atual está incorreta.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var passwordResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        
        if (!passwordResult.Succeeded)
        {
            var errors = passwordResult.Errors.Select(e => e.Description).ToList();
            return Error.Failure("User.PasswordUpdateFailed", string.Join(", ", errors));
        }

        return new ResetPasswordResponse
        {
            Success = true,
            Message = "Senha alterada com sucesso."
        };
    }
}
