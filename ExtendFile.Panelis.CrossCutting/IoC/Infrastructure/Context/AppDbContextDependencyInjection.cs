using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Context;

public static class AppDbContextDependencyInjection
{
    public static void AddAppDbContextDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DomainEventsPublishInterceptor>();
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<DomainEventsPublishInterceptor>();
            options.UseNpgsql(connectionString)
                .AddInterceptors(interceptor);
        });
    }
}