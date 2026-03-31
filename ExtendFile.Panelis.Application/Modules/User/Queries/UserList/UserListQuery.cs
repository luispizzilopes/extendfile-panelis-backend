using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Queries.UserList;

public record UserListQuery(UserListRequest Request) : IRequest<ErrorOr<PaginedResult<UserListResponse>>>;
