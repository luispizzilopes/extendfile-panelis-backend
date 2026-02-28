using ExtendFile.Panelis.Presentation.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class RequestPipelineConfigurationExtension
{
    public static void AddRequestPipelineConfigurations(this WebApplication app)
    {
        if (!app.Environment.IsProduction())
        {
            app.ConfigureSwagger();
        }

        app.UseCors(c =>
        {
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.AllowAnyOrigin();
        });
        
        app.UseHttpsRedirection();
        app.UseAuthConfiguration();
        app.UseMiddleware<ErrorMiddleware>();
        app.MapControllers();
    }
    
    private static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json", 
                    description.GroupName.ToUpperInvariant());
            }
            options.DefaultModelsExpandDepth(-1);
        });
    }
    
    private static void UseAuthConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}