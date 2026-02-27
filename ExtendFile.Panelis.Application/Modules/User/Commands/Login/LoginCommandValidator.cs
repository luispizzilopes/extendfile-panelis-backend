using FluentValidation;
using ExtendFile.Panelis.Application.Modules.User.Requests.Login;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .EmailAddress()
            .WithMessage("Email deve ser um endereço de email válido");

        RuleFor(x => x.Request.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("Senha deve ter pelo menos 6 caracteres");
    }
}
