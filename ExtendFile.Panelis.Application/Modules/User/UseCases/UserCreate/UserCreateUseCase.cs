using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.UserCreate;

public class UserCreateUseCase
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public UserCreateUseCase(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<UserCreateResponse>> ExecuteAsync(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return Error.Conflict("User.EmailAlreadyExists", "O e-mail informado já está em uso.");

        var generatedPassword = string.Empty.GenerateRandomPassword();

        var user = new Domain.Modules.User.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email,
            Active = request.Active
        };

        var result = await _userManager.CreateAsync(user, generatedPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Error.Failure("User.CreateFailed", string.Join(", ", errors));
        }

        return new UserCreateResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email!,
            GeneratedPassword = generatedPassword,
            Active = user.Active ?? false
        };
    }
}