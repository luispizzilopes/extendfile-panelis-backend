using ExtendFile.Panelis.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<Domain.Modules.User.Entities.User> _userManager;

    public AuthenticationService(UserManager<Domain.Modules.User.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> PasswordSignInAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return false;
        
        return await _userManager.CheckPasswordAsync(user, password);
    }
}