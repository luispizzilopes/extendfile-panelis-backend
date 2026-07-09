using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExtendFile.Panelis.Presentation.Swagger;

[AttributeUsage(AttributeTargets.Method)]
public sealed class RequiresApiKeyAttribute : Attribute;

public class ApiKeyOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAttribute = context.MethodInfo
            .GetCustomAttributes(typeof(RequiresApiKeyAttribute), true)
            .Any();

        if (!hasAttribute) return;

        operation.Security ??= [];
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "X-Api-Key"
                    }
                },
                []
            }
        });
    }
}
