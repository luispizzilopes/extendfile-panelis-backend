using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class ActionResultExtension
{
    public static IActionResult ToActionResult<T>(this T result, ControllerBase controller)
    {
        if(result is null) return controller.NoContent();
        
        return controller.Ok(result);
    }    
    
    public static IActionResult ToActionResult<T>(this ErrorOr<T> result, ControllerBase controller)
    {
        if (result.Value is null && result.IsError == false) 
            return controller.NoContent();
        
        if (!result.IsError)
        {
            return !(result.Value is Success) ?
                controller.Ok(result.Value) :
                controller.Ok("Operação realizada com sucesso!");
        }

        var error = result.FirstError;
        return error.Type switch
        {
            ErrorType.Failure => controller.StatusCode(500, error),
            ErrorType.NotFound => controller.NotFound(error),
            ErrorType.Validation => controller.BadRequest(error),
            _ => controller.StatusCode(500, error),
        };
    }
}