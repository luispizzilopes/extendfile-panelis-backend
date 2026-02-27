using ExtendFile.Panelis.Domain.Modules.User.Entities;
using ExtendFile.Panelis.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Identity;

public static class IdentityDependencyInjection
{
    public static void AddIdentityDependencyInjection(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1)); 
    }
}