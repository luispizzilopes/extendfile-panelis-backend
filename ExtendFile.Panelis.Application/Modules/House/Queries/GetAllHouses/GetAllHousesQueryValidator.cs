using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetAllHouses;

public class GetAllHousesQueryValidator : AbstractValidator<GetAllHousesQuery>
{
    public GetAllHousesQueryValidator()
    {
        // No validation needed for GetAllHousesQuery as it has no parameters
    }
}
