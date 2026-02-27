using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;
using IAuthenticationService = Microsoft.AspNetCore.Authentication.IAuthenticationService;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Services;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenJwtService, TokenJwtService>();
    }
}