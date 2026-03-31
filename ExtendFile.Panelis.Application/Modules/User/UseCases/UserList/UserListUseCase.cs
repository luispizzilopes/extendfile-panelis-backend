using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.User.UseCases.UserList;

public class UserListUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserListUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginedResult<UserListResponse>>> ExecuteAsync(UserListRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetUsersAsync(request.PaginationParams, request.Name, request.Email, cancellationToken);
        
        var userResponses = users.Data?
            .Select(u => new UserListResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email!,
                Picture = u.Picture,
                Active = u.Active,
            }).ToList();

        return new PaginedResult<UserListResponse>
        {
            Data = userResponses,
            TotalRecords = users.TotalRecords,
            PageNumber = users.PageNumber,
            PageSize = users.PageSize
        };
    }
}
