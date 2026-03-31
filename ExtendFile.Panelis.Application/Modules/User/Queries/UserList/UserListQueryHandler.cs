using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserList;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Queries.UserList;

public class UserListQueryHandler : IRequestHandler<UserListQuery, ErrorOr<PaginedResult<UserListResponse>>>
{
    private readonly UserListUseCase _userListUseCase;

    public UserListQueryHandler(UserListUseCase userListUseCase)
    {
        _userListUseCase = userListUseCase;
    }

    public async Task<ErrorOr<PaginedResult<UserListResponse>>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        return await _userListUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
