using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.User;
using ExtendFile.Panelis.Domain.Modules.User.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.UserUpdate;

public class UserUpdateUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public UserUpdateUseCase(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<UserUpdateResponse>> ExecuteAsync(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        
        if (user is null)
            return Error.NotFound("User.NotFound", "Usuário não encontrado.");

        if (!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Error.Conflict("User.EmailAlreadyExists", "O e-mail informado já está em uso por outro usuário.");
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.UserName = request.Email;
        user.Active = request.Active;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Error.Failure("User.UpdateFailed", string.Join(", ", errors));
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
            
            if (!passwordResult.Succeeded)
            {
                var errors = passwordResult.Errors.Select(e => e.Description).ToList();
                return Error.Failure("User.PasswordUpdateFailed", string.Join(", ", errors));
            }
        }

        return new UserUpdateResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email!,
            Active = user.Active ?? false
        };
    }
}
