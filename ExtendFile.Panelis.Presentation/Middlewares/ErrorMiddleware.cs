using System.Net;
using ErrorOr;
using ExtendFile.Panelis.Presentation.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExtendFile.Panelis.Presentation.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ErrorMiddleware(RequestDelegate next,  IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var error = Error.Failure(
            code: "UnexpectedError",
            description: ex.InnerException?.Message ?? ex.Message
        );

        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var result = JsonConvert.SerializeObject(error, settings);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(result);
    }
}
