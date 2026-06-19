using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.VerifyPasswordResetCode;

public class VerifyPasswordResetCodeCommandValidator : AbstractValidator<VerifyPasswordResetCodeCommand>
{
    public VerifyPasswordResetCodeCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .EmailAddress().WithMessage("E-mail inválido");

        RuleFor(x => x.Request.Code)
            .NotEmpty().WithMessage("Código é obrigatório")
            .Length(6).WithMessage("Código deve ter 6 dígitos")
            .Matches(@"^\d{6}$").WithMessage("Código deve conter apenas números");
    }
}
