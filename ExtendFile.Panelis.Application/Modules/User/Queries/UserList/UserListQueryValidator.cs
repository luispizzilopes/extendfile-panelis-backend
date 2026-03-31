using FluentValidation;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.User.Queries.UserList;

public class UserListQueryValidator : AbstractValidator<UserListQuery>
{
    public UserListQueryValidator()
    {
        RuleFor(x => x.Request.Name)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Request.Name))
            .WithMessage("Nome não pode ter mais de 200 caracteres");

        RuleFor(x => x.Request.Email)
            .MaximumLength(255)
            .When(x => !string.IsNullOrEmpty(x.Request.Email))
            .WithMessage("Email não pode ter mais de 255 caracteres");

        RuleFor(x => x.Request.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Request.Email))
            .WithMessage("Email deve ser um endereço de email válido");
    }
}
