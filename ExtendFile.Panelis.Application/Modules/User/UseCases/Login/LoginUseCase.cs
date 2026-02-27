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

    public LoginUseCase(
        UserManager<Domain.Modules.User.Entities.User> userManager, 
        SignInManager<Domain.Modules.User.Entities.User> signInManager,
        ITokenJwtService tokenJwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenJwtService = tokenJwtService;
    }

    public async Task<ErrorOr<UserSessionResponse>> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Error.NotFound("User.NotFound", "Usuário não encontrado");
        }

        if (!user.Active.HasValue || !user.Active.Value)
        {
            return Error.Validation("User.Inactive", "Usuário está inativo");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            return Error.Validation("Login.InvalidCredentials", "Email ou senha inválidos");
        }

        var tokenJwtInformation = _tokenJwtService.CreateTokenUser(user);
        
        return new UserSessionResponse(user.Id, user.Email, tokenJwtInformation);
    }
}
