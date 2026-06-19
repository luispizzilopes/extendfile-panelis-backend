using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandValidator : AbstractValidator<RequestPasswordResetCommand>
{
    public RequestPasswordResetCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório")
            .EmailAddress()
            .WithMessage("E-mail inválido");
    }
}
