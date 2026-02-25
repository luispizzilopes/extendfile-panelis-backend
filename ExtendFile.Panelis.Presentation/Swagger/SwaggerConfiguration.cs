using ExtendFile.Panelis.Presentation.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace ExtendFile.Panelis.Presentation.Swagger;

public static class SwaggerConfiguration
{
     public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings();
        
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{apiSettings.Title}",
                Version = $"v{apiSettings.Version}",
                Description = $"Documentação da versão {apiSettings.Version} da {apiSettings.Description}",
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Digite o token JWT com o prefixo 'Bearer'. Exemplo: Bearer {seu token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });

            var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            
            var applicationXmlFilename = "ExtendFile.Panelis.application.xml";
            var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFilename);
            if (File.Exists(applicationXmlPath))
            {
                options.IncludeXmlComments(applicationXmlPath);
            }
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}