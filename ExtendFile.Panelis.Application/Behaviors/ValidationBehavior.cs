using ErrorOr;
using FluentValidation;
using MediatR;

namespace ExtendFile.Panelis.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next(cancellationToken);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (_validator is null || validationResult.IsValid)
        {
            return await next(cancellationToken);
        }

        var errors = validationResult.Errors
            .ConvertAll(validation => Error.Validation(validation.PropertyName, validation.ErrorMessage));

        return (dynamic)errors;
    }
}
