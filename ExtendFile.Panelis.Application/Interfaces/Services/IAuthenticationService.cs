namespace ExtendFile.Panelis.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<bool> PasswordSignInAsync(string email, string password);
}